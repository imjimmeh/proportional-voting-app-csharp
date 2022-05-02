using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Tokens.Service
{
    public interface ITokenService
    {
        Task<string> GenerateToken(IUserWithClaims user);
    }
}