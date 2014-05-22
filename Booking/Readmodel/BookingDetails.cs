using System;
using System.ComponentModel.DataAnnotations;

namespace Booking.Readmodel
{
    public class BookingDetails
    {
        [Key]
        public Guid BookingId { get; set; }

        public int CarId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public BookingDetails()
        {
        }

        public BookingDetails(Guid bookingId, int carId,DateTime bookingStartDate,DateTime bookingEndDate, bool isCancelled,DateTime createdDate,DateTime modifieDate  )
        {
            this.BookingId = bookingId;
            this.CarId = carId;
            this.BookingStartDate=BookingStartDate;
            this.BookingEndDate = BookingEndDate;
            this.IsCancelled = isCancelled;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifieDate;
        }
    }
}
