using CarApp.Data;
using CarApp.Models;
using CarApp.Services.Interfaces;

namespace CarApp.Services
{
    public class CarBrandService : BaseService<CarBrand>, ICarBrandService
    {
        private readonly ApplicationDbContext _db;

        public CarBrandService(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
    }
}
