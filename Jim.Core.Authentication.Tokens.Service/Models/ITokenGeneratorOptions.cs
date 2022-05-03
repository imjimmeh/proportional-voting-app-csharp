using Jim.Core.Authentication.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security;

namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public interface ITokenGeneratorOptions
    {
        string Issuer { get; }
        string PrivateKey { get; }
        string PublicKey { get; }
        string Audience { get; }
        TimeSpan TimeToLive { get; init; }

        string Algorithm { get; }
    }
}