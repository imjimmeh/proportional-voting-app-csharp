using Jim.Core.Authentication.Models.Interfaces;
using System.Security.Claims;

namespace Jim.Core.Authentication.Models.Services
{
    /// <summary>
    /// Signs users in/out
    /// </summary>
    /// <typeparam name="TUser">Type of user to accept</typeparam>
    public interface IUserSignInManager<TUser> where TUser : class, IUserWithClaims
    {
        ClaimsPrincipal? User { get; }

        /// <summary>
        /// Creates relevant ClaimsPrincipal,ClaimsIdentity, Claims, etc. for the given user, then adds to their HttpContext
        /// </summary>
        /// <param name="user">User to create identity for</param>
        /// <param name="authenticationType">How the user authenticated</param>
        /// <returns></returns>
        Task SignInAsync(TUser user, string authenticationType);

        /// <summary>
        /// Check if the user is authetnicated or not, and return their username if so
        /// </summary>
        /// <returns></returns>
        (bool isAuthenticated, string? userName) TryGetUserUsername();
    }
}