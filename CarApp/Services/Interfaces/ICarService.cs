using CarApp.Models;
using CarApp.Models.Dto;

namespace CarApp.Services.Interfaces
{
    public interface ICarService : IBaseService<Car>
    {

        Task<List<Car>> GetCarsByBrandAsync(int brandId);
    }
}
