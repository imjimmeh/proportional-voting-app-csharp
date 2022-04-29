using Jim.Core.Authentication.Models.Interfaces;
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

            var userClaims = user.Claims.Select(claim => new Claim(claim.ClaimType, claim.Value)).ToList();
            userClaims.Insert(0, new Claim(ClaimTypes.NameIdentifier, user.Username.ToString()));
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