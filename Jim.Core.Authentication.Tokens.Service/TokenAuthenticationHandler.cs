using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Tokens.Service.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Jim.Core.Authentication.Tokens.Service
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationHandlerOptions>
    {
        private readonly ITokenService _tokenService;
        
        public TokenAuthenticationHandler(ITokenService tokenService, IOptionsMonitor<TokenAuthenticationHandlerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.TryGetValue(Options.AuthenticationHeader, out StringValues foundHeader) || foundHeader.Count != 1)
                return AuthenticateResult.NoResult();

            var parsedHeader = foundHeader.FirstOrDefault();

            if(string.IsNullOrEmpty(parsedHeader))
                return AuthenticateResult.NoResult();

            var tokenValid = _tokenService.ValidateTokenResult(parsedHeader);


            var claims = new[] { new Claim(ClaimTypes.Name, "TestUserNameClaim") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
