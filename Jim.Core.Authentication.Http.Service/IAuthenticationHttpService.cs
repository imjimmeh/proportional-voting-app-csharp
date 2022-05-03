using Jim.Core.Authentication.Models.DTOs;

namespace Jim.Core.Authentication.Http.Service
{
    public interface IAuthenticationHttpService
    {
        Task<TokenLoginResponse> LoginAsync(LoginRequest login);
    }
}