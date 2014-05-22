using System;
using Infrastructure.EventSourcing;

namespace Booking.Contract.Events
{
    public class BookingRequestPlaced : VersionedEvent
    {
        public int CarId{ get; set; }

        public DateTime BookingStartDate { get; set; }

        public DateTime BookingEndDate { get; set; }
    }
}
