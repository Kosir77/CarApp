using CarApp.Data;
using CarApp.Models;
using CarApp.Services.Interfaces;

namespace CarApp.Services
{
    public class CarService : BaseService<Car>, ICarService
    {
        private readonly ApplicationDbContext _db;

        public CarService(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public async Task<Car> UpdateCar(Car car)
        {
            car.UpdatedAt = DateTime.Now;
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
            return car;
        }
    }
}
