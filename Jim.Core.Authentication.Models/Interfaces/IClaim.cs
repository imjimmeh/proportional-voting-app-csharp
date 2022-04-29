namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IClaim
    {
        string ClaimType { get; }
        string Value { get; set; }
    }
}