using Jim.Core.Authentication.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Jim.Core.Authentication.Service
{
    public class UserSignInManager<TUser> : IUserSignInManager<TUser> where TUser : class, IDatabaseUser
    {
        private IHttpContextAccessor _contextAccessor;

        public UserSignInManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public HttpContext Context => _contextAccessor.HttpContext;

        public async Task SignInAsync(TUser user, string authenticationType)
        {
            var context = Context;

            var userIsAuthenticated = context.User?.Identity.IsAuthenticated ?? false;

            if (userIsAuthenticated)
                throw new Exception($"User is already authenticated as {context.User!.Identity?.Name}");

            var getUserClaims = user.Claims.Select(c => new Claim(c.ClaimTypeValue, c.Value));

            var userClaims = ClaimsService.ConvertUserClaims(user);

            var newIdentity = new ClaimsIdentity(userClaims, authenticationType);

            context.User = new ClaimsPrincipal(newIdentity);

            context.User.AddIdentity(newIdentity);
        }

        public (bool isAuthenticated, string? userName) TryGetUserUsername()
        {
            var userName = _contextAccessor.HttpContext.User?.Identity?.Name;

            return (_contextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false, userName);
        }
    }
}
