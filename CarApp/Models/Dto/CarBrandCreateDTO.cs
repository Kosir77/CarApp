using System.ComponentModel.DataAnnotations;

namespace CarApp.Models.Dto
{
    public class CarBrandCreateDTO
    {
        [Required]
        public string BrandName { get; set; }
    }
}
