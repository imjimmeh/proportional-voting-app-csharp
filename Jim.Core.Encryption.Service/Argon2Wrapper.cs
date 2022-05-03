using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Jim.Core.Encryption.Models;

namespace Jim.Core.Encryption.Service
{
    internal class Argon2Wrapper : IDisposable
    {
        private bool _disposed = false;

        public Argon2Wrapper()
        {
        }

        public bool VerifyHashedString(Argon2EncryptionOptions options, string toVerify, string hashed, string salt)
        {
            SecureArray<byte>? hashB = null;
            try
            {
                var config = options.ToConfig(toVerify, salt);

                var matching = Argon2.Verify(hashed, toVerify, options.Secret);

                return matching;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verifying hash", ex);
            }
            finally
            {
                hashB?.Dispose();
            }
        }

        protected internal HashedStringWithSalt ConcreteHashString(Argon2EncryptionOptions options, string toHash, byte[] salt) => ConcreteHashString(options, toHash, Convert.ToBase64String(salt));

        protected internal HashedStringWithSalt ConcreteHashString(Argon2EncryptionOptions options, string toHash, string salt)
        {
            try
            {
                var config = options.ToConfig(toHash, salt);

                using (var argon2 = new Argon2(config))
                {
                    using (SecureArray<byte> hashA = argon2.Hash())
                    {
                        var hashed = config.EncodeString(hashA.Buffer);
                        return new HashedStringWithSalt(hashed, salt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error hashing string - {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GC.Collect();
            }
        }
    }
}
