using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Tokens.Service.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

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

            if (user == null)
                throw new ArgumentNullException(nameof(user));
            try
            {
                using var signingCredentialsGenerator = new SigningCredentialsGenerator(Options);

                var descriptor = signingCredentialsGenerator.CreateDescriptor(user);

                var token = JwtSecurityTokenHandler.CreateToken(descriptor);

                var tokenString = JwtSecurityTokenHandler.WriteToken(token);

                return new TokenResult
                {
                    GeneratedToken = tokenString,
                    ExpiresAt = descriptor.Expires
                };
            }
            catch (ArgumentNullException)
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
            if (string.IsNullOrEmpty(token))
                return new ValidateTokenResult(false);

            try
            {
                using RSA rsa = RSA.Create();
                rsa.ImportFromPem(SigningCredentialsGenerator.CleanKey(Options.PublicKey));

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Options.Issuer,
                    ValidAudience = Options.Audience,
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    CryptoProviderFactory = new CryptoProviderFactory()
                    {
                        CacheSignatureProviders = false
                    }
                };

                var claimsPrincipal = JwtSecurityTokenHandler.ValidateToken(token, validationParameters, out var validatedSecurityToken);
                return new ValidateTokenResult(true);
            }
            catch (Exception ex)
            {
                return new ValidateTokenResult(false);
            }
        }

    }
}