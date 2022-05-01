using Jim.Core.Encryption.Models;

namespace Jim.Core.Encryption.Service
{
    public interface IEncryptionService
    {
        HashedStringWithSalt HashString(string toHash);


        bool VerifyHashedString(string incoming, HashedStringWithSalt expected);
    }
}