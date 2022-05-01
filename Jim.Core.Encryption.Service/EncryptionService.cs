using Isopoh.Cryptography.Argon2;
using Jim.Core.Encryption.Models;

namespace Jim.Core.Encryption.Service
{
    public abstract class EncryptionService : IEncryptionService
    {
        public HashedStringWithSalt HashString(string toHash)
        {
            if(!VerifyString(toHash))
            {
                throw new Exception($"Not a valid string");
            }

            return ConcreteHashString(toHash);
        }

        protected internal virtual bool VerifyString(string toHash)
         => !string.IsNullOrEmpty(toHash);

        protected internal abstract HashedStringWithSalt ConcreteHashString(string toHash);

    }
}