using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Models.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {
        Task<TokenResult> GenerateToken(IUserWithClaims user);
        Task<ValidateTokenResult> ValidateTokenResult(string token);
    }
}