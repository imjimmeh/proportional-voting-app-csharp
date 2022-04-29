namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IClaim
    {
        string ClaimTypeValue { get; }
        string Value { get; set; }
    }
}