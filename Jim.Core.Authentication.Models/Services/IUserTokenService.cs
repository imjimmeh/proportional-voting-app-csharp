using Jim.Core.Authentication.Models.DTOs;

namespace Jim.Core.Authentication.Models.Services
{
    public interface IUserTokenService
    {
        Task<TokenLoginResponse?> TryGenerateUserToken(LoginRequest login);
    }
}