using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Booking.Readmodel;
using Infrastructure.BlobStorage;
using Infrastructure.Messaging;
using Infrastructure.Serialization;
using Infrastructure.Sql.BlobStorage;
using Infrastructure.Sql.Messaging;
using Infrastructure.Sql.Messaging.Implementation;
using Microsoft.Practices.Unity;
using CarRentals;

namespace CarRentalsAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IUnityContainer container;
        protected void Application_Start()
        {
            #if AZURESDK
            Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.Changed +=
                (s, a) =>
                {
                    Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.RequestRecycle();
                };
            #endif
            
            DatabaseSetup.Initialize();

            this.container = CreateContainer();

           GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(this.container);

            #if AZURESDK
            if (Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.IsAvailable)
            {
                System.Diagnostics.Trace.Listeners.Add(new Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener());
                System.Diagnostics.Trace.AutoFlush = true;
            }
            #endif

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            try
            {
                // repositories used by the application

                container.RegisterType<CarRenatlsContext>(new TransientLifetimeManager(), new InjectionConstructor("CarRentals"));

                var cache = new MemoryCache("ReadModel");
                container.RegisterType<IBookingDao, BookingDao>();
                //container.RegisterType<IBookingDao, BookingDao>(
                //    new ContainerControlledLifetimeManager(),
                //    new InjectionConstructor(new ResolvedParameter<BookingDao>(), cache));

                // configuration specific settings

                OnCreateContainer(container);

                return container;
            }
            catch
            {
                container.Dispose();
                throw;
            }
        }

        private static void OnCreateContainer(UnityContainer container)
        {
            var serializer = new JsonTextSerializer();
            container.RegisterInstance<ITextSerializer>(serializer);

            container.RegisterType<IBlobStorage, SqlBlobStorage>(new ContainerControlledLifetimeManager(), new InjectionConstructor("BlobStorage"));
            container.RegisterType<IMessageSender, MessageSender>(
                "Commands", new TransientLifetimeManager(), new InjectionConstructor(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Commands"));
            container.RegisterType<ICommandBus, CommandBus>(
                new ContainerControlledLifetimeManager(), new InjectionConstructor(new ResolvedParameter<IMessageSender>("Commands"), serializer));
        }
    }
}
