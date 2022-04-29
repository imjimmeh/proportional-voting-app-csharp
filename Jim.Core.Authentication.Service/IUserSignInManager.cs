using Jim.Core.Authentication.Models.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Jim.Core.Authentication.Service
{
    public interface IUserSignInManager<TUser> where TUser : class, IDatabaseUser
    {
        HttpContext Context { get; }

        Task SignInAsync(TUser user, string authenticationType);
        (bool isAuthenticated, string? userName) TryGetUserUsername();
    }
}