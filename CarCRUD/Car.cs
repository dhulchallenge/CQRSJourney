using System.ComponentModel.DataAnnotations;

namespace CarRentals
{
    public class Car
    {
        public Car()
        {

        }

        public Car(string modelName)
        {

            this.CarModel = modelName;
        }
        [Key]
        public string CarId { get; set; }
        [Display(Name = "CarModel")]
        public string CarModel { get; set; }
        [Display(Name = "CreatedDate")]
        public string CreatedDate { get; set; }
        [Display(Name = "ModifiedDate")]
        public decimal ModifiedDate { get; set; }
    }
}
