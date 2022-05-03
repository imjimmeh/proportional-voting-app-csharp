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
        public string PublicKey { get; init; }
        public string PrivateKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public TimeSpan TimeToLive { get; init; }
        public string Algorithm { get; init; }
    }
}