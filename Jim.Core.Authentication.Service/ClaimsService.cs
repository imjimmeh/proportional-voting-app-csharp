using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Microsoft.AspNetCore.Http;

namespace Jim.Core.Authentication.Service
{
    public class ClaimsService<TUser> : IClaimsService
        where TUser : class, IUserWithClaims
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserSignInManager<TUser> _signInManager;
        public ClaimsService(IHttpContextAccessor httpContextAccessor, IUserSignInManager<TUser> signInManager)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public HttpContext Context => _httpContextAccessor.HttpContext;

        public IEnumerable<string>? GetUserClaimsForType(string type)
        {
            if (_signInManager.User == null)
                throw new UnauthorizedAccessException("User not authenticated");

            return _signInManager.User.Claims.Where(claim => claim.Type == type)
                                             .Select(claim => claim.Value);
        }
    }
}
