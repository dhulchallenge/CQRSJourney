using System.Data.Entity;
using System.Linq;
using Booking;
using Booking.Database;
using Booking.Handlers;
using Booking.Readmodel;
using Microsoft.Practices.Unity;

namespace WorkerRoleCommandProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Infrastructure;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Handling;
    using Infrastructure.Processes;
    using Infrastructure.Serialization;
    using Infrastructure.Sql.Database;
    using Infrastructure.Sql.Processes;

    public sealed partial class CarBookingProcessor : IDisposable
    {
        private IUnityContainer container;
        private CancellationTokenSource cancellationTokenSource;
        private List<IProcessor> processors;
        private bool instrumentationEnabled;

        public CarBookingProcessor(bool instrumentationEnabled = false)
        {
            this.instrumentationEnabled = instrumentationEnabled;

            OnCreating();

            this.cancellationTokenSource = new CancellationTokenSource();
            this.container = CreateContainer();

            this.processors = this.container.ResolveAll<IProcessor>().ToList();
        }

        public void Start()
        {
            this.processors.ForEach(p => p.Start());
        }

        public void Stop()
        {
            this.cancellationTokenSource.Cancel();

            this.processors.ForEach(p => p.Stop());
        }

        public void Dispose()
        {
            this.container.Dispose();
            this.cancellationTokenSource.Dispose();
        }

        private UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            // infrastructure
            container.RegisterInstance<ITextSerializer>(new JsonTextSerializer());
            container.RegisterInstance<IMetadataProvider>(new StandardMetadataProvider());

            container.RegisterType<DbContext, BookingProcessManagerDbContext>("booking", new TransientLifetimeManager(), new InjectionConstructor("BookingProcess"));
            container.RegisterType<IProcessManagerDataContext<BookingProcessManager>, SqlProcessManagerDataContext<BookingProcessManager>>(
                new TransientLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<Func<DbContext>>("booking"), typeof(ICommandBus), typeof(ITextSerializer)));


            container.RegisterType<BookingDbContext>(new TransientLifetimeManager(), new InjectionConstructor("Booking"));

            container.RegisterType<IBookingDao, BookingDao>(new ContainerControlledLifetimeManager());
            
            // handler
            container.RegisterType<ICommandHandler, BookCarHandler>("BookCarHandler");
            //container.RegisterType<ICommandHandler, UpdateAvailablityHandler>("UpdateAvailablityHandler");
            // Conference management integration
            container.RegisterType<global::CarRentals.CarRenatlsContext>(new TransientLifetimeManager(), new InjectionConstructor("CarRentals"));

            OnCreateContainer(container);

            return container;
        }

        partial void OnCreating();
        partial void OnCreateContainer(UnityContainer container);
    }
}
