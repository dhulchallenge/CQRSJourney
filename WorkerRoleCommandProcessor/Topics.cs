using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRoleCommandProcessor
{
    public static class Topics
    {
        public static class Commands
        {
            /// <summary>
            /// conference/commands
            /// </summary>
            public const string Path = "carrentals/commands";

            public static class Subscriptions
            {
                /// <summary>
                /// sessionless
                /// </summary>
                public const string Sessionless = "sessionless";
                /// <summary>
                /// seatsavailability
                /// </summary>
                public const string Seatsavailability = "seatsavailability";
                /// <summary>
                /// log
                /// </summary>
                public const string Log = "log";
            }
        }

        public static class Events
        {
            /// <summary>
            /// conference/events
            /// </summary>
            public const string Path = "carrentals/events";

            public static class Subscriptions
            {
                /// <summary>
                /// log
                /// </summary>
                public const string Log = "log";
                /// <summary>
                /// Registration.RegistrationPMNextSteps
                /// </summary>
                public const string RegistrationPMNextSteps = "Registration.RegistrationPMNextSteps";
                /// <summary>
                /// Registration.PricedOrderViewModelGeneratorV3
                /// </summary>
                public const string PricedOrderViewModelGeneratorV3 = "Registration.PricedOrderViewModelGeneratorV3";
                /// <summary>
                /// Registration.ConferenceViewModelGenerator
                /// </summary>
                public const string ConferenceViewModelGenerator = "Registration.ConferenceViewModelGenerator";
            }
        }

        public static class EventsOrders
        {
            /// <summary>
            /// conference/eventsOrders
            /// </summary>
            public const string Path = "carrentals/eventsOrders";

            public static class Subscriptions
            {
                /// <summary>
                /// logOrders
                /// </summary>
                public const string LogOrders = "logOrders";
                /// <summary>
                /// Registration.RegistrationPMOrderPlacedOrders
                /// </summary>
                public const string RegistrationPMOrderPlacedOrders = "Registration.RegistrationPMOrderPlacedOrders";
                /// <summary>
                /// Registration.RegistrationPMNextStepsOrders
                /// </summary>
                public const string RegistrationPMNextStepsOrders = "Registration.RegistrationPMNextStepsOrders";
                /// <summary>
                /// Registration.OrderViewModelGeneratorOrders
                /// </summary>
                public const string OrderViewModelGeneratorOrders = "Registration.OrderViewModelGeneratorOrders";
                /// <summary>
                /// Registration.PricedOrderViewModelOrders
                /// </summary>
                public const string PricedOrderViewModelOrders = "Registration.PricedOrderViewModelOrders";
                /// <summary>
                /// Registration.SeatAssignmentsViewModelOrders
                /// </summary>
                public const string SeatAssignmentsViewModelOrders = "Registration.SeatAssignmentsViewModelOrders";
                /// <summary>
                /// Registration.SeatAssignmentsHandlerOrders
                /// </summary>
                public const string SeatAssignmentsHandlerOrders = "Registration.SeatAssignmentsHandlerOrders";
                /// <summary>
                /// Conference.OrderEventHandlerOrders
                /// </summary>
                public const string OrderEventHandlerOrders = "Conference.OrderEventHandlerOrders";
            }
        }

        public static class EventsAvailability
        {
            /// <summary>
            /// conference/eventsAvailability
            /// </summary>
            public const string Path = "carrentals/eventsAvailability";

            public static class Subscriptions
            {
                /// <summary>
                /// logAvail
                /// </summary>
                public const string LogAvail = "logAvail";
                /// <summary>
                /// Registration.RegistrationPMNextStepsAvail
                /// </summary>
                public const string RegistrationPMNextStepsAvail = "Registration.RegistrationPMNextStepsAvail";
                /// <summary>
                /// Registration.ConferenceViewModelAvail
                /// </summary>
                public const string ConferenceViewModelAvail = "Registration.ConferenceViewModelAvail";
            }
        }

    }
}
