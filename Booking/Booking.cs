using System;
using System.Collections.Generic;
using Booking.Contract.Events;
using Booking.Events;
using Infrastructure.EventSourcing;

namespace Booking
{
    public class Booking : EventSourced
    {
        private Guid bookingId;
        private int CarId;
        private DateTime BookingStartDate;
        private DateTime BookingEndDate;
        public Booking(Guid id) : base(id)
        {
            base.Handles<BookingRequestPlaced>(this.OnBookingRequestPlaced);
            base.Handles<CarFounded>(this.OnCarFounded);
        }



        public Booking(Guid id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            this.LoadFrom(history);
        }


        public Booking(Guid id, int carId, DateTime bookingStartDate,DateTime bookingEndDate)
            : this(id)
        {
            CarId = carId;
            BookingStartDate = bookingStartDate;
            BookingEndDate = bookingEndDate;
            this.Update(new BookingRequestPlaced
            {
                CarId = carId,
                SourceId = id,
                BookingStartDate = bookingStartDate,
                BookingEndDate = bookingEndDate
            });
        }

        public void MakeBooking(Guid BookingId, int CarId, DateTime bookingStartDate, DateTime bookingEndDate)
        {
            var CarFounded = new CarFounded(BookingId) { CarId = CarId, BookingStartDate = bookingStartDate, BookingEndDate = bookingEndDate };
            base.Update(CarFounded);
        }

        public void AssignCar(int carId)
        {
           Update(
               new CarFounded(this.Id) { CarId = carId, BookingStartDate = BookingStartDate, BookingEndDate = BookingEndDate });
        }

        private void OnBookingRequestPlaced(BookingRequestPlaced obj)
        {
            this.CarId = obj.CarId;
            this.bookingId = obj.SourceId;
            this.BookingStartDate = obj.BookingStartDate;
            this.BookingEndDate = obj.BookingEndDate;
        }

        private void OnCarFounded(CarFounded obj)
        {
            this.CarId = obj.CarId;
            this.bookingId = obj.SourceId;
            this.BookingStartDate = obj.BookingStartDate;
            this.BookingEndDate = obj.BookingEndDate;
        }

    }
}
