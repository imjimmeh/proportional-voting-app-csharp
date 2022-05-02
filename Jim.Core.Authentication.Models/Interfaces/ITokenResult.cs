namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface ITokenResult
    {
        DateTime? ExpiresAt { get; init; }
        string? GeneratedToken { get; init; }
    }
}