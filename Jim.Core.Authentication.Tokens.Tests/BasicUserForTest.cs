using Jim.Core.Authentication.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Jim.Core.Authentication.Tokens.Tests
{
    public record BasicUserForTest : IUserWithClaims
    {
        public IEnumerable<IClaim> Claims { get; set; } = new List<IClaim>();

        public string Username { get; set; } = null!;

        public long Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
