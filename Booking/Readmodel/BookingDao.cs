using System;
using System.Linq;
using Booking.Contract;

namespace Booking.Readmodel
{
    public class BookingDao : IBookingDao
    {
        private readonly Func<BookingDbContext> contextFactory;

        public BookingDao(Func<BookingDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public bool IsCarAvailable(int CarId)
        {
            using (var context = contextFactory.Invoke())
            {
                var availableCarsCount = context.Query<CarsAvailable>().Where(A => A.CarId == CarId).Select(s => s.Count).SingleOrDefault();
                if (availableCarsCount <= 0)
                    return false;
                else
                    return true;

            };
        }

        public BookingDetails GetBookingDetails(Guid bookingId)
        {
            using (var context = contextFactory.Invoke())
            {
                var bookingDetails = context.Query<BookingDetails>().Where(b => b.BookingId == bookingId).Single();
                return bookingDetails;

            }
        }
    }
}
