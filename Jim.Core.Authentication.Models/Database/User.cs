using Jim.Core.Authentication.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("Users")]
    public class User : IUserWithClaims
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public IList<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

        [NotMapped]
        public IEnumerable<IClaim> Claims => UserClaims.Select(userClaim => userClaim.Claim);
    }
}