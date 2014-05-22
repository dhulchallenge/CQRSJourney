using System.Collections.Generic;
using Booking.Contract;
using Infrastructure.EventSourcing;

namespace Booking.Events
{
    public class AvailabilityCarsChanged :VersionedEvent
    {
        public IEnumerable<CarsAvailable> AvailableCars { get; set; }
    }
}
