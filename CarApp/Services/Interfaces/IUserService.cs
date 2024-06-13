using CarApp.Models;
using CarApp.Models.Dto;

namespace CarApp.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        
    }
}
