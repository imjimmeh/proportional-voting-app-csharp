using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Service;
using Jim.Core.Authentication.Tokens.Service.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Jim.Core.Authentication.Tokens.Service
{
    public class TokenGenerator : TokenService, ITokenGenerator
    {
        public TokenGenerator(ITokenGeneratorOptions options) : base(options)
        {
        }

        public string GenerateTokenForUser(IDatabaseUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var userClaims = ClaimsService.ConvertUserClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = Options.Issuer,
                Audience = Options.Audience,
                SigningCredentials = null//new SigningCredentials();
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}