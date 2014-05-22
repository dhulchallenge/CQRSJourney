using System;
using Infrastructure.EventSourcing;

namespace Booking.Events
{
    public class BookingCreated : VersionedEvent
    {
        public Guid BookingId { get; set; }

        public int carid { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
