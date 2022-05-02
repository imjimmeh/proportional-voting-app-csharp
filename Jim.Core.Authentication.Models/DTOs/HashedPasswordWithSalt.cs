using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Encryption.Models;

namespace Jim.Core.Authentication.Models.DTOs
{
    public record HashedPasswordWithSalt : IHashedPasswordWithSalt
    {
        public HashedPasswordWithSalt(HashedStringWithSalt stringWithSalt)
        {
            HashedPassword = stringWithSalt.HashedString;
            PasswordSalt = stringWithSalt.Salt;
        }

        public HashedPasswordWithSalt(string hashedPassword, string passwordSalt)
        {
            HashedPassword = hashedPassword ?? throw new ArgumentNullException(nameof(hashedPassword));
            PasswordSalt = passwordSalt ?? throw new ArgumentNullException(nameof(passwordSalt));
        }

        public string HashedPassword { get; init; }

        public string PasswordSalt { get; init; }
    }
}
