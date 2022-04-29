using Jim.Core.Authentication.Models.DTOs;

namespace Jim.Core.Authentication.Models.Services
{
    public interface IUserManagerService
    {
        Task<bool> CreateNewUser(CreateUserDTO newUser);
    }
}