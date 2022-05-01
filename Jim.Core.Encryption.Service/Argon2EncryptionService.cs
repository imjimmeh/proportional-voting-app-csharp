using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Jim.Core.Encryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        protected internal override HashedStringWithSalt ConcreteHashString(string toHash)
        {
            try
            {
                var salt = CreateSalt();
                var config = _options.ToConfig(toHash, salt);

                using (var argon2 = new Argon2(config))
                {
                    using (SecureArray<byte> hashA = argon2.Hash())
                    {
                        var hashed = config.EncodeString(hashA.Buffer);

                        return new HashedStringWithSalt(hashed, Encoding.UTF8.GetString(salt));
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Error hashing string - {ex.Message}");
            }
        }

        protected internal byte[] CreateSalt()
        {
            byte[] salt = new byte[16];
            _rng.GetBytes(salt);

            return salt;
        }
    }
}
