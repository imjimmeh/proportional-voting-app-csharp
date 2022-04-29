using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("UserClaims")]
    public class UserClaim
    {
        public long UserId { get; set; }

        public long ClaimId { get; set; }

        public User User { get; set; } = null!;

        public JimClaim Claim { get; set; } = null!;
    }
}
