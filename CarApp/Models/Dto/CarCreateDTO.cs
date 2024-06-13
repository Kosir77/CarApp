using System.ComponentModel.DataAnnotations;

namespace CarApp.Models.Dto
{
    public class CarCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int EnginePower { get; set; }
        public CarTransmission Transmission { get; set; }
        public int NumberOfOwners { get; set; }
        public FuelType FuelType { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
