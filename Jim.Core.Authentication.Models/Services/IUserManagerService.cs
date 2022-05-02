using Jim.Core.Authentication.Models.DTOs;

namespace Jim.Core.Authentication.Models.Services
{
    /// <summary>
    /// Service to register and manage users
    /// </summary>
    public interface IUserManagerService : ILoginService
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="newUser">User to create</param>
        /// <returns>Created UserId</returns>
        Task<long?> CreateNewUser(CreateUserDTO newUser);
    }
}