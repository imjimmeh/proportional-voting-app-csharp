namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public interface ITokenGeneratorOptions
    {
        string Issuer { get; }
        string PrivateKey { get; }
        string PublicKey { get; }
        string Audience { get; }
    }
}