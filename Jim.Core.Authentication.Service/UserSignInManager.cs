using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Jim.Core.Authentication.Service
{
    public class UserSignInManager<TUser> : IUserSignInManager<TUser> where TUser : class, IUserWithClaims
    {
        private IHttpContextAccessor _contextAccessor;

        public UserSignInManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public HttpContext Context => _contextAccessor.HttpContext;

        public ClaimsPrincipal? User => Context.User;

        public virtual async Task SignInAsync(TUser user, string authenticationType)
        {
            SignIn(user, authenticationType);
        }

        public virtual void SignIn(TUser user, string authenticationType)
        {
            var context = Context;

            var userIsAuthenticated = context.User?.Identity?.IsAuthenticated ?? false;

            if (userIsAuthenticated)
                throw new Exception($"User is already authenticated as {context.User!.Identity?.Name}");

            var getUserClaims = user.Claims.Select(c => new Claim(c.ClaimTypeValue, c.Value));

            var userClaims = ClaimsHelpers.ConvertUserClaims(user);

            var newIdentity = new ClaimsIdentity(userClaims, authenticationType);

            context.User = new ClaimsPrincipal(newIdentity);

            context.User.AddIdentity(newIdentity);
        }

        public virtual (bool isAuthenticated, string? userName) TryGetUserUsername()
        {
            var userName = _contextAccessor.HttpContext.User?.Identity?.Name;

            return (_contextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false, userName);
        }
    }
}
