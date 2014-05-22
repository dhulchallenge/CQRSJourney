using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Conference.Common.Entity;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.Diagnostics.Management;
using Microsoft.WindowsAzure.ServiceRuntime;
using LogLevel = Microsoft.WindowsAzure.Diagnostics.LogLevel;

namespace WorkerRoleCommandProcessor
{
    public class WorkerRole : RoleEntryPoint
    {
        private bool running;

        public override void Run()
        {
            TaskScheduler.UnobservedTaskException += this.OnUnobservedTaskException;
            this.running = true;

            while (this.running)
            {
                Trace.WriteLine("Starting the command processor", "Information");
                using (var processor = new CarBookingProcessor(this.InstrumentationEnabled))
                {
                    processor.Start();

                    while (this.running)
                    {
                        Thread.Sleep(10000);
                    }

                    processor.Stop();

                    // cause the process to recycle
                    return;
                }
            }

            TaskScheduler.UnobservedTaskException -= this.OnUnobservedTaskException;
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Trace.TraceError("Unobserved task exception: \r\n{0}", e.Exception);
        }

        public override bool OnStart()
        {
            RoleEnvironment.Changing += (sender, e) =>
            {
                if (e.Changes
                        .OfType<RoleEnvironmentConfigurationSettingChange>()
                        .Any(x => x.ConfigurationSettingName != MaintenanceMode.MaintenanceModeSettingName))
                {
                    Trace.TraceInformation("Recycling worker role because of configuration change");
                    e.Cancel = true;
                }
            };
            RoleEnvironment.Changed += (sender, e) =>
            {
                if (e.Changes
                        .OfType<RoleEnvironmentConfigurationSettingChange>()
                        .Any(x => x.ConfigurationSettingName == MaintenanceMode.MaintenanceModeSettingName))
                {
                    Trace.TraceInformation("Refreshing maintenance mode because of configuration change");
                    MaintenanceMode.RefreshIsInMaintainanceMode();
                }
            };
            MaintenanceMode.RefreshIsInMaintainanceMode();

            var config = DiagnosticMonitor.GetDefaultInitialConfiguration();

            var cloudStorageAccount =
                CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));

            TimeSpan transferPeriod;
            if (!TimeSpan.TryParse(RoleEnvironment.GetConfigurationSettingValue("Diagnostics.ScheduledTransferPeriod"), out transferPeriod))
            {
                transferPeriod = TimeSpan.FromMinutes(1);
            }

            TimeSpan sampleRate;
            if (!TimeSpan.TryParse(RoleEnvironment.GetConfigurationSettingValue("Diagnostics.PerformanceCounterSampleRate"), out sampleRate))
            {
                sampleRate = TimeSpan.FromSeconds(30);
            }

            LogLevel logLevel;
            if (!Enum.TryParse<LogLevel>(RoleEnvironment.GetConfigurationSettingValue("Diagnostics.LogLevelFilter"), out logLevel))
            {
                logLevel = LogLevel.Verbose;
            }

            // Setup performance counters
            config.PerformanceCounters.DataSources.Add(
                new PerformanceCounterConfiguration
                {
                    CounterSpecifier = @"\Processor(_Total)\% Processor Time",
                    SampleRate = sampleRate
                });

#if !LOCAL
            foreach (var counterName in
                new[] 
                { 
                    Infrastructure.Azure.Instrumentation.SessionSubscriptionReceiverInstrumentation.TotalSessionsCounterName,
                    Infrastructure.Azure.Instrumentation.SessionSubscriptionReceiverInstrumentation.CurrentSessionsCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.TotalMessagesCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.TotalMessagesSuccessfullyProcessedCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.TotalMessagesUnsuccessfullyProcessedCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.TotalMessagesCompletedCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.TotalMessagesNotCompletedCounterName,                
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.MessagesReceivedPerSecondCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.AverageMessageProcessingTimeCounterName,
                    Infrastructure.Azure.Instrumentation.SubscriptionReceiverInstrumentation.CurrentMessagesInProcessCounterName,
                })
            {
                config.PerformanceCounters.DataSources.Add(
                    new PerformanceCounterConfiguration
                    {
                        CounterSpecifier = @"\" + Infrastructure.Azure.Instrumentation.Constants.ReceiversPerformanceCountersCategory + @"(*)\" + counterName,
                        SampleRate = sampleRate
                    });
            }

            foreach (var counterName in
                new[] 
                { 
                    Infrastructure.Azure.Instrumentation.EventStoreBusPublisherInstrumentation.CurrentEventPublishersCounterName,
                    Infrastructure.Azure.Instrumentation.EventStoreBusPublisherInstrumentation.EventPublishingRequestsPerSecondCounterName,
                    Infrastructure.Azure.Instrumentation.EventStoreBusPublisherInstrumentation.EventsPublishedPerSecondCounterName,
                    Infrastructure.Azure.Instrumentation.EventStoreBusPublisherInstrumentation.TotalEventsPublishedCounterName,
                    Infrastructure.Azure.Instrumentation.EventStoreBusPublisherInstrumentation.TotalEventsPublishingRequestsCounterName,
                })
            {
                config.PerformanceCounters.DataSources.Add(
                    new PerformanceCounterConfiguration
                    {
                        CounterSpecifier = @"\" + Infrastructure.Azure.Instrumentation.Constants.EventPublishersPerformanceCountersCategory + @"(*)\" + counterName,
                        SampleRate = sampleRate
                    });
            }
#endif

            config.PerformanceCounters.ScheduledTransferPeriod = transferPeriod;

            // Setup logs
            config.Logs.ScheduledTransferPeriod = transferPeriod;
            config.Logs.ScheduledTransferLogLevelFilter = logLevel;

            // use the current deployment ID
            var diagnosticsManager = new DeploymentDiagnosticManager(RoleEnvironment.GetConfigurationSettingValue("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"), RoleEnvironment.DeploymentId);
            var thisInstance = RoleEnvironment.CurrentRoleInstance;
            var instanceManager =
                (from manager in diagnosticsManager.GetRoleInstanceDiagnosticManagersForRole(thisInstance.Role.Name)
                 where manager.RoleInstanceId == thisInstance.Id
                 select manager).Single();

            instanceManager.SetCurrentConfiguration(config);

            //DiagnosticMonitor.Start(RoleEnvironment.GetConfigurationSettingValue("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"), config);

            Trace.Listeners.Add(new Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener());
            Trace.AutoFlush = true;

            Database.DefaultConnectionFactory = new ServiceConfigurationSettingConnectionFactory(Database.DefaultConnectionFactory);
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private bool InstrumentationEnabled
        {
            get
            {
                bool instrumentationEnabled;
                if (!bool.TryParse(RoleEnvironment.GetConfigurationSettingValue("InstrumentationEnabled"), out instrumentationEnabled))
                {
                    instrumentationEnabled = false;
                }

                return instrumentationEnabled;
            }
        }
    }
}
