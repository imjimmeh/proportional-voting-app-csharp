using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("UserClaims")]
    public class UserClaim
    {
        public long UserId { get; set; } //Composite key

        public long ClaimId { get; set; } //Composite key

        public User User { get; set; } = null!;

        public JimClaim Claim { get; set; } = null!;
    }
}
