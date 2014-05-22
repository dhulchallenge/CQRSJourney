namespace Booking.Contract
{
    using System.ComponentModel.DataAnnotations;
    public class CarsAvailable
    {

        public CarsAvailable(int carId, int availableCount)
        {
            this.CarId = carId;
            this.Count = availableCount;
        }

        [Key]
        public int Id { get; set; }

        public int CarId{ get; set; }

        public int Count{ get; set; }
        
    }
}
