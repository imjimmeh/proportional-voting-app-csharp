using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Tokens.Service.Models
{
    public interface ITokenGenerator
    {
        string GenerateTokenForUser(IDatabaseUser user);
    }
}