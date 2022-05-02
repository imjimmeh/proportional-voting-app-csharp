using Jim.Core.Authentication.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security;

namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public interface ITokenGeneratorOptions
    {
        string Issuer { get; }
        SecureString PrivateKey { get; }
        SecureString PublicKey { get; }
        string Audience { get; }
        TimeSpan TimeToLive { get; init; }

        public SecurityTokenDescriptor CreateDescriptor(IUserWithClaims user);
        SigningCredentials GenerateSigningCredentials();
    }
}