using System;
using Infrastructure.EventSourcing;

namespace Booking.Contract.Events
{
    public class CarFounded : VersionedEvent
    {
        public CarFounded(Guid Id)
        {
            this.SourceId = Id;
        }

        public int CarId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
