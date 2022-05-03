using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Service;
using Jim.Core.Authentication.Tokens.Service.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Jim.Core.Authentication.Tokens.Service
{
    public class SigningCredentialsGenerator : IDisposable
    {
        private bool _disposing = false;
        private readonly ITokenGeneratorOptions _options;
        private RSA? _rsa;

        public SigningCredentialsGenerator(ITokenGeneratorOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public SecurityTokenDescriptor CreateDescriptor(IUserWithClaims user)
             => new SecurityTokenDescriptor
             {
                 Subject = new ClaimsIdentity(user.ConvertUserClaims()),
                 Expires = DateTime.UtcNow + _options.TimeToLive,
                 Issuer = _options.Issuer,
                 Audience = _options.Audience,
                 SigningCredentials = GenerateSigningCredentials()
             };

        public SigningCredentials GenerateSigningCredentials()
        {
            try
            {
                _rsa = RSA.Create();
                var key = CleanKey(_options.PrivateKey);

                _rsa.ImportFromPem(_options.PrivateKey.ToCharArray());

                return new SigningCredentials(new RsaSecurityKey(_rsa), _options.Algorithm)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating signing credentials", ex);
            }
        }

        public static string CleanKey(string key)
        {
            var parsedKey = !key.StartsWith("-----") ? key : key.Split("-----", StringSplitOptions.RemoveEmptyEntries)[1];
            parsedKey = key.Trim();
            parsedKey = key.Replace(Environment.NewLine, "");
            parsedKey = key.Replace(" ", "");
            parsedKey = string.Concat(key.SkipWhile(c => c >= 'A' && c <= 'Z' ||
                                                  c >= 'a' && c <= 'z' ||
                                                  c >= '0' && c <= '9' ||
                                                  c == '+' ||
                                                  c == '/'));

            return key;
        } 

        public void Dispose()
        {
            if (!_disposing)
            {
                _disposing = true;
                _rsa?.Dispose();
                GC.Collect();
            }
        }
    }
}
