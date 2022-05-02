using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Service;
using Jim.Core.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public readonly struct TokenGeneratorOptions : ITokenGeneratorOptions
    {
        public SecureString PublicKey { get; init; }
        public SecureString PrivateKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public TimeSpan TimeToLive { get; init; }
        public string SecurityAlgorithm { get; init; }

        public SecurityTokenDescriptor CreateDescriptor(IUserWithClaims user)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(user.ConvertUserClaims()),
                Expires = DateTime.UtcNow + TimeToLive,
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = GenerateSigningCredentials()
            };
        }

        public SigningCredentials GenerateSigningCredentials()
        {
            var privateKey = PrivateKey?.ToString()?.ToByteArray();

            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);

            return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithm)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };
        }
    }
}