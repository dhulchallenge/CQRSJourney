using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Azure.Messaging;
using Infrastructure.Messaging.Handling;
using Infrastructure.Serialization;
using Microsoft.Practices.Unity;

namespace WorkerRoleCommandProcessor
{
    public static class UnityContainerExtensions
    {
        public static void RegisterEventProcessor<T>(this IUnityContainer container, ServiceBusConfig busConfig, string subscriptionName, bool instrumentationEnabled = false)
            where T : IEventHandler
        {
            container.RegisterInstance<IProcessor>(subscriptionName, busConfig.CreateEventProcessor(
                subscriptionName,
                container.Resolve<T>(),
                container.Resolve<ITextSerializer>(),
                instrumentationEnabled));
        }
    }
}
