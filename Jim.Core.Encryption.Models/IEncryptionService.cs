namespace Jim.Core.Encryption.Models
{
    public interface IEncryptionService
    {
        HashedStringWithSalt HashString(string toHash);
        HashedStringWithSalt HashString(string toHash, string salt);

        bool VerifyHashedString(string incoming, HashedStringWithSalt expected);
    }
}