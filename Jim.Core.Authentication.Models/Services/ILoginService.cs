using Jim.Core.Authentication.Models.DTOs;

namespace Jim.Core.Authentication.Models.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Checks that the username exists, then validates that hte password supplied matches the one on record
        /// If all true, returns a stripped down DTO of the User, otherwise throws an exception
        /// </summary>
        /// <param name="loginRequest">Incoming login request for user</param>
        /// <returns>GetUserDTO of user details if found, otherwise an exception of some form</returns>
        Task<GetUserDTO?> TryLoginAsync(LoginRequest loginRequest);
    }
}