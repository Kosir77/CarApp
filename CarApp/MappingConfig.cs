using AutoMapper;
using CarApp.Models;
using CarApp.Models.Dto;

namespace CarApp
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Car, CarCreateDTO>().ReverseMap();
            CreateMap<CarBrand, CarBrandDTO>().ReverseMap();
            CreateMap<CarBrand, CarBrandCreateDTO>().ReverseMap();
        }
    }
}
