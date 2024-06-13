using CarApp.Data;
using CarApp.Models;
using CarApp.Models.Dto;
using CarApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Services
{
    public class CarService : BaseService<Car>, ICarService
    {
        private readonly ApplicationDbContext _db;

        public CarService(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public async Task<List<Car>> GetCarsByBrandAsync(int brandId)
        {
            return await _db.Cars.Where(c => c.BrandId == brandId).ToListAsync();
        }

    }
}
