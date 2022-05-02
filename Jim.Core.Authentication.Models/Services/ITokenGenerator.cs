using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Models.Services
{
    public interface ITokenGenerator
    {
        string GenerateTokenForUser(IUserWithClaims user);
    }
}