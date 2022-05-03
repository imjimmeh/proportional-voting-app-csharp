using Microsoft.AspNetCore.Authentication;

namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public class TokenAuthenticationHandlerOptions : AuthenticationSchemeOptions
    {
        public string AuthenticationHeader { get; set; } = null!;
    }
}
