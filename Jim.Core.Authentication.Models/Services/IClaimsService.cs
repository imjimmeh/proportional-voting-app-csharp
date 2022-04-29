
namespace Jim.Core.Authentication.Models.Services
{
    public interface IClaimsService
    { 
        IEnumerable<string>? GetUserClaimsForType(string type);
    }
}