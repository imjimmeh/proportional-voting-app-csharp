namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IHashedPasswordWithSalt
    {
        public string HashedPassword { get; set; }

        public string PasswordSalt { get; set; }
    }
}
