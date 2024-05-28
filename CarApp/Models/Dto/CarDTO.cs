using System.ComponentModel.DataAnnotations;

namespace CarApp.Models.Dto
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
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
