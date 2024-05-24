using CarApp.Models;

namespace CarApp.Services.Interfaces
{
    public interface ICarService : IBaseService<Car>
    {

        Task<Car> UpdateCar(Car car);
    }
}
