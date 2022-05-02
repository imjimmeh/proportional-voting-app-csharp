namespace Jim.Core.Encryption.Models
{
    public interface IEncryptionService
    {
        HashedStringWithSalt HashString(string toHash);


        bool VerifyHashedString(string incoming, HashedStringWithSalt expected);
    }
}