namespace Jim.Core.Encryption.Models
{
    public record struct HashedStringWithSalt
    {
        public HashedStringWithSalt()
        {
        }

        public HashedStringWithSalt(string hashedString, string salt)
        {
            HashedString = !string.IsNullOrEmpty(hashedString) ? hashedString : throw new ArgumentNullException(nameof(hashedString));
            Salt = !string.IsNullOrEmpty(salt) ? salt : throw new ArgumentNullException(nameof(salt));
        }

        public string HashedString { get; init; } = null!;

        public string Salt { get; init; } = null!;
    }
}