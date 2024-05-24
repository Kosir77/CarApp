using System.ComponentModel.DataAnnotations;

namespace CarApp.Models.Dto
{
    public class CarCreateDTO
    {
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
