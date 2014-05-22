using System;
using Infrastructure.Messaging;

namespace Booking.Command
{
    public class MakeBooking : CarAvailabiltyCommand 
    {
        public MakeBooking(int carId) : base(carId)
        {
            CarId = carId;
        }
        public DateTime BookingStartdate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
