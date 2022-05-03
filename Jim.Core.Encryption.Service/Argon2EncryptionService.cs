using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Jim.Core.Encryption.Models;
using System.Security.Cryptography;
using System.Text;

namespace Jim.Core.Encryption.Service
{
    public class Argon2EncryptionService : EncryptionService
    {
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        private readonly Argon2EncryptionOptions _options;

        public Argon2EncryptionService(Argon2EncryptionOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override bool VerifyHashedString(string toVerify, HashedStringWithSalt expected)
        {
            using var argon2Wrapper = new Argon2Wrapper();
            return argon2Wrapper.VerifyHashedString(_options, toVerify, expected.HashedString, expected.Salt);
        }

        protected internal override HashedStringWithSalt ConcreteHashString(string toHash)
        {
            var salt = CreateSalt();
            return ConcreteHashString(toHash, salt);
        }

        protected internal HashedStringWithSalt ConcreteHashString(string toHash, byte[] salt) => ConcreteHashString(toHash, Convert.ToBase64String(salt));

        protected internal override HashedStringWithSalt ConcreteHashString(string toHash, string salt)
        {
            using var argon2Wrapper = new Argon2Wrapper();
            return argon2Wrapper.ConcreteHashString(_options, toHash, salt);
        }

        protected internal byte[] CreateSalt()
        {
            byte[] salt = new byte[16];
            _rng.GetBytes(salt);

            return salt;
        }
    }
}
