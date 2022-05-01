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
            try
            {
                var config = _options.ToConfig(toVerify, Encoding.ASCII.GetBytes(expected.Salt));

                if (config.DecodeString(expected.HashedString, out SecureArray<byte>? hashB) && hashB != null)
                {
                    var argon2ToVerify = new Argon2(config);
                    using (var hashToVerify = argon2ToVerify.Hash())
                    {
                        var equals = Argon2.FixedTimeEquals(hashB, hashToVerify);

                        return equals;
                    }
                }

                return false;
            }
            catch(ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verifying hash", ex);
            }
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

                        return new HashedStringWithSalt(hashed, Encoding.ASCII.GetString(salt));
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
