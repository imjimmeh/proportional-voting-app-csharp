using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Service;
using Jim.Core.Authentication.Tokens.Service.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Jim.Core.Authentication.Tokens.Service
{
    public class TokenService : ITokenService
    {
        private readonly ITokenGeneratorOptions _options;
        private JwtSecurityTokenHandler? _jwtSecurityTokenHandler;

        public TokenService(ITokenGeneratorOptions options)
        {
            _options = options;
        }

        protected ITokenGeneratorOptions Options => _options;
        protected JwtSecurityTokenHandler JwtSecurityTokenHandler => _jwtSecurityTokenHandler ??= new JwtSecurityTokenHandler();

        public async Task<string> GenerateToken(IUserWithClaims user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var descriptor = _options.CreateDescriptor(user);

                var token = JwtSecurityTokenHandler.CreateToken(descriptor);

                return JwtSecurityTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating token for user {user.Username}", ex);
            }
        }
    }
}