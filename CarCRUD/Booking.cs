using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentals
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }
        [Display(Name="CarId")]
        public int CarId { get; set; }

        [Display(Name = "BookingStartDate")]
        public DateTime BookingStartDate { get; set; }

        [Display(Name="BookingEndDate")]
        public DateTime BookingEndDate { get; set; }

        [Display(Name = "IsCanceled")]
        public bool IsCanceled { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }
}
