using Jim.Core.Authentication.Tokens.Service.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Jim.Core.Authentication.Tokens.Service
{
    public class TokenService
    {
        private readonly ITokenGeneratorOptions _options;
        private JwtSecurityTokenHandler? _jwtSecurityTokenHandler;

        public TokenService(ITokenGeneratorOptions options)
        {
            _options = options;
        }

        protected ITokenGeneratorOptions Options => _options;
        protected JwtSecurityTokenHandler JwtSecurityTokenHandler => _jwtSecurityTokenHandler ??= new JwtSecurityTokenHandler();
    }
}