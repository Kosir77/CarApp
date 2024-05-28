using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Models
{
    public class CarBrand
    {
        [Key] 
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
