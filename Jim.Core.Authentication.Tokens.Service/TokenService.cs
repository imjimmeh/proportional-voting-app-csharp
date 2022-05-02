using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Tokens.Service.Models;
using System.IdentityModel.Tokens.Jwt;

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

        public async Task<TokenResult> GenerateToken(IUserWithClaims user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var descriptor = _options.CreateDescriptor(user);

                var token = JwtSecurityTokenHandler.CreateToken(descriptor);

                var tokenString = JwtSecurityTokenHandler.WriteToken(token);

                return new TokenResult
                {
                    GeneratedToken = tokenString,
                    ExpiresAt = descriptor.Expires
                };
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating token for user {user.Username}", ex);
            }
        }

        public async Task<ValidateTokenResult> ValidateTokenResult(string token)
        {
            return new ValidateTokenResult(true);
        }
    }
}