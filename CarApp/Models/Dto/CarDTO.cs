using System.ComponentModel.DataAnnotations;

namespace CarApp.Models.Dto
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
