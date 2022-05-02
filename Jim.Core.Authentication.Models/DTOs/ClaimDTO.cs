using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Models.DTOs
{
    public struct ClaimDTO : IClaim
    {
        public string Type { get; init; }

        public string Value { get; set;}
    }
}
