using Jim.Core.Authentication.Models.DTOs;

namespace Jim.Core.Authentication.Models.Services
{
    /// <summary>
    /// Service to register and manage users
    /// </summary>
    public interface IUserManagerService
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="newUser">User to create</param>
        /// <returns>bool if success - otherwise exception</returns>
        Task<bool> CreateNewUser(CreateUserDTO newUser);
    }
}