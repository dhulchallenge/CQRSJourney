using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using Booking;
using Booking.Handlers;
using Infrastructure;
using Infrastructure.Azure;
using Infrastructure.Azure.BlobStorage;
using Infrastructure.Azure.EventSourcing;
using Infrastructure.Azure.Instrumentation;
using Infrastructure.Azure.MessageLog;
using Infrastructure.Azure.Messaging;
using Infrastructure.Azure.Messaging.Handling;
using Infrastructure.BlobStorage;
using Infrastructure.EventSourcing;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Handling;
using Infrastructure.Serialization;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure;

namespace WorkerRoleCommandProcessor
{
    public partial class CarBookingProcessor
    {
        private InfrastructureSettings azureSettings;
        private ServiceBusConfig busConfig;

        partial void OnCreating()
        {
            this.azureSettings = InfrastructureSettings.Read("Settings.xml");
            this.busConfig = new ServiceBusConfig(this.azureSettings.ServiceBus);

            busConfig.Initialize();
        }

        partial void OnCreateContainer(UnityContainer container)
        {
            var metadata = container.Resolve<IMetadataProvider>();
            var serializer = container.Resolve<ITextSerializer>();

            // blob
            var blobStorageAccount = CloudStorageAccount.Parse(azureSettings.BlobStorage.ConnectionString);
            container.RegisterInstance<IBlobStorage>(new CloudBlobStorage(blobStorageAccount, azureSettings.BlobStorage.RootContainerName));

            var commandBus = new CommandBus(new TopicSender(azureSettings.ServiceBus, Topics.Commands.Path), metadata, serializer);
            var eventsTopicSender = new TopicSender(azureSettings.ServiceBus, Topics.Events.Path);
            container.RegisterInstance<IMessageSender>("events", eventsTopicSender);
            container.RegisterInstance<IMessageSender>("booking", new TopicSender(azureSettings.ServiceBus, Topics.EventsOrders.Path));
            container.RegisterInstance<IMessageSender>("caravailability", new TopicSender(azureSettings.ServiceBus, Topics.EventsAvailability.Path));
            var eventBus = new EventBus(eventsTopicSender, metadata, serializer);

            var sessionlessCommandProcessor =
                new CommandProcessor(new SubscriptionReceiver(azureSettings.ServiceBus, Topics.Commands.Path, Topics.Commands.Subscriptions.Sessionless, false, new SubscriptionReceiverInstrumentation(Topics.Commands.Subscriptions.Sessionless, this.instrumentationEnabled)), serializer);
            var seatsAvailabilityCommandProcessor =
                new CommandProcessor(new SessionSubscriptionReceiver(azureSettings.ServiceBus, Topics.Commands.Path, Topics.Commands.Subscriptions.Seatsavailability, false, new SessionSubscriptionReceiverInstrumentation(Topics.Commands.Subscriptions.Seatsavailability, this.instrumentationEnabled)), serializer);

            var synchronousCommandBus = new SynchronousCommandBusDecorator(commandBus);
            container.RegisterInstance<ICommandBus>(synchronousCommandBus);

            container.RegisterInstance<IEventBus>(eventBus);
            container.RegisterInstance<IProcessor>("SessionlessCommandProcessor", sessionlessCommandProcessor);
            container.RegisterInstance<IProcessor>("SeatsAvailabilityCommandProcessor", seatsAvailabilityCommandProcessor);

            RegisterRepositories(container);
            RegisterEventProcessors(container);
            RegisterCommandHandlers(container, sessionlessCommandProcessor, seatsAvailabilityCommandProcessor);

            // handle order commands inline, as they do not have competition.
            synchronousCommandBus.Register(container.Resolve<ICommandHandler>("BookCarHandler"));

            // message log
            var messageLogAccount = CloudStorageAccount.Parse(azureSettings.MessageLog.ConnectionString);

            container.RegisterInstance<IProcessor>("EventLogger", new AzureMessageLogListener(
                new AzureMessageLogWriter(messageLogAccount, azureSettings.MessageLog.TableName),
                new SubscriptionReceiver(azureSettings.ServiceBus, Topics.Events.Path, Topics.Events.Subscriptions.Log)));

            container.RegisterInstance<IProcessor>("BookingEventLogger", new AzureMessageLogListener(
                new AzureMessageLogWriter(messageLogAccount, azureSettings.MessageLog.TableName),
                new SubscriptionReceiver(azureSettings.ServiceBus, Topics.EventsOrders.Path, Topics.EventsOrders.Subscriptions.LogOrders)));

            container.RegisterInstance<IProcessor>("CarAvailabilityEventLogger", new AzureMessageLogListener(
                new AzureMessageLogWriter(messageLogAccount, azureSettings.MessageLog.TableName),
                new SubscriptionReceiver(azureSettings.ServiceBus, Topics.EventsAvailability.Path, Topics.EventsAvailability.Subscriptions.LogAvail)));

            container.RegisterInstance<IProcessor>("CommandLogger", new AzureMessageLogListener(
                new AzureMessageLogWriter(messageLogAccount, azureSettings.MessageLog.TableName),
                new SubscriptionReceiver(azureSettings.ServiceBus, Topics.Commands.Path, Topics.Commands.Subscriptions.Log)));
        }

        private void RegisterEventProcessors(UnityContainer container)
        {
            container.RegisterType<BookingProcessManagerRouter>(new ContainerControlledLifetimeManager());

            container.RegisterEventProcessor<BookingProcessManagerRouter>(this.busConfig, Topics.Events.Subscriptions.RegistrationPMNextSteps, this.instrumentationEnabled);
        }

        private static void RegisterCommandHandlers(IUnityContainer unityContainer, ICommandHandlerRegistry sessionlessRegistry, ICommandHandlerRegistry seatsAvailabilityRegistry)
        {
            var commandHandlers = unityContainer.ResolveAll<ICommandHandler>().ToList();
            var seatsAvailabilityHandler = commandHandlers.First(x => x.GetType().IsAssignableFrom(typeof(UpdateAvailablityHandler)));

            seatsAvailabilityRegistry.Register(seatsAvailabilityHandler);
            foreach (var commandHandler in commandHandlers.Where(x => x != seatsAvailabilityHandler))
            {
                sessionlessRegistry.Register(commandHandler);
            }
        }

        private void RegisterRepositories(UnityContainer container)
        {
            // repository
            var eventSourcingAccount = CloudStorageAccount.Parse(this.azureSettings.EventSourcing.ConnectionString);
            var ordersEventStore = new EventStore(eventSourcingAccount, this.azureSettings.EventSourcing.OrdersTableName);
            var seatsAvailabilityEventStore = new EventStore(eventSourcingAccount, this.azureSettings.EventSourcing.SeatsAvailabilityTableName);

            container.RegisterInstance<IEventStore>("booking", ordersEventStore);
            container.RegisterInstance<IPendingEventsQueue>("booking", ordersEventStore);

            container.RegisterInstance<IEventStore>("caravailability", seatsAvailabilityEventStore);
            container.RegisterInstance<IPendingEventsQueue>("caravailability", seatsAvailabilityEventStore);

            container.RegisterType<IEventStoreBusPublisher, EventStoreBusPublisher>(
                "booking",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IMessageSender>("booking"),
                    new ResolvedParameter<IPendingEventsQueue>("booking"),
                    new EventStoreBusPublisherInstrumentation("worker - booking", this.instrumentationEnabled)));
            container.RegisterType<IEventStoreBusPublisher, EventStoreBusPublisher>(
                "caravailability",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IMessageSender>("caravailability"),
                    new ResolvedParameter<IPendingEventsQueue>("caravailability"),
                    new EventStoreBusPublisherInstrumentation("worker - caravailability", this.instrumentationEnabled)));

            var cache = new MemoryCache("RepositoryCache");

            container.RegisterType<IEventSourcedRepository<Booking.Booking>, AzureEventSourcedRepository<Booking.Booking>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IEventStore>("booking"),
                    new ResolvedParameter<IEventStoreBusPublisher>("booking"),
                    typeof(ITextSerializer),
                    typeof(IMetadataProvider),
                    cache));

            container.RegisterType<IEventSourcedRepository<CarAvailability>, AzureEventSourcedRepository<CarAvailability>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IEventStore>("booking"),
                    new ResolvedParameter<IEventStoreBusPublisher>("booking"),
                    typeof(ITextSerializer),
                    typeof(IMetadataProvider),
                    cache));

            
            // to satisfy the IProcessor requirements.
            container.RegisterInstance<IProcessor>(
                "BookingEventStoreBusPublisher",
                new PublisherProcessorAdapter(container.Resolve<IEventStoreBusPublisher>("booking"), this.cancellationTokenSource.Token));
            container.RegisterInstance<IProcessor>(
                "CarsAvailabilityEventStoreBusPublisher",
                new PublisherProcessorAdapter(container.Resolve<IEventStoreBusPublisher>("caravailability"), this.cancellationTokenSource.Token));
        }

        // to satisfy the IProcessor requirements.
        // TODO: we should unify and probably use token-based Start only processors.
        private class PublisherProcessorAdapter : IProcessor
        {
            private IEventStoreBusPublisher publisher;
            private CancellationToken token;

            public PublisherProcessorAdapter(IEventStoreBusPublisher publisher, CancellationToken token)
            {
                this.publisher = publisher;
                this.token = token;
            }

            public void Start()
            {
                this.publisher.Start(this.token);
            }

            public void Stop()
            {
                // Do nothing. The cancelled token will stop the process anyway.
            }
        }
    }
}
