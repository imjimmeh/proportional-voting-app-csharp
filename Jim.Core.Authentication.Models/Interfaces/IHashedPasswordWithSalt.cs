namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IHashedPasswordWithSalt
    {
        public string HashedPassword { get; }

        public string PasswordSalt { get; }
    }
}
