namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IDatabaseUser : IUserWithClaims, IHashedPasswordWithSalt
    {
        public void WithPassword(IHashedPasswordWithSalt passwordAndSalt);
    }
}
