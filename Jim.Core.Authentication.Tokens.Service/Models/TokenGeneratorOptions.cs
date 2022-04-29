namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public readonly struct TokenGeneratorOptions : ITokenGeneratorOptions
    {
        public string PublicKey { get; init; }
        public string PrivateKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
    }
}