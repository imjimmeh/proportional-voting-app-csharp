using Jim.Core.Authentication.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Authentication.Models.DTOs
{
    public record struct TokenResult : ITokenResult
    {
        public DateTime? ExpiresAt { get; init; }
        public string? GeneratedToken { get; init; }
    }
}
