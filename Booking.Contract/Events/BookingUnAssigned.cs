using System;
using Infrastructure.EventSourcing;

namespace Booking.Contract.Events
{
    public class BookingUnAssigned : VersionedEvent
    {
        public BookingUnAssigned(Guid sourceId)
        {
            this.SourceId = sourceId;
        }

        public int CarId { get; set; }
    }
}
