namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IClaim
    {
        string Type { get; }
        string Value { get; set; }
    }
}