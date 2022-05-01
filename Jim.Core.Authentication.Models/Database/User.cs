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
        [Required]
        public string Username { get; set; } = null!;
        
        [Required]
        public string HashedPassword { get; set; } = null!;

        [Required]
        public string PasswordSalt { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastModified { get; set; }

        public IList<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

        [NotMapped]
        public IEnumerable<IClaim> Claims => UserClaims.Select(userClaim => userClaim.Claim);
    }
}