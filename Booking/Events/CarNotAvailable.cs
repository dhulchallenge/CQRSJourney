using Infrastructure.EventSourcing;
using System;

namespace Booking.Events
{
    public class CarNotAvailable : VersionedEvent
    {
        public Guid BookingId { get; set; }
        public int Carid { get; set; }
    }
}
