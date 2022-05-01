namespace Jim.Core.Encryption.Models
{
    public record Argon2EncryptionOptions : EncryptionOptions
    {
        public Argon2EncryptionOptions()
        {
        }

        public Argon2EncryptionOptions(string secret) : base(secret)
        {
        }

        protected Argon2EncryptionOptions(EncryptionOptions original) : base(original)
        {
        }
    }
}
