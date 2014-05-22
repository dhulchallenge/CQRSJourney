using System;
using Infrastructure.Messaging;

namespace Booking.Command
{
    /// <summary>
    /// Booking the car if available
    /// </summary>
    public class BookCar : ICommand, IMessageSessionProvider
    {
        public BookCar()
        {
            BookingId = Guid.NewGuid();
        }
        public int CarId { get; set; }
        public Guid BookingId { get; set; }

        /// <summary>
        /// Booking availed from
        /// </summary>
        public DateTime BookingStartDate { get; set; }
        /// <summary>
        /// Returning the car
        /// </summary>
        public DateTime BookingEndDate { get; set; }

        public Guid Id { get; private set; }
        public string SessionId { get; private set; }
    }
}
