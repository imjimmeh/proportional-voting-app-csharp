using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("ClaimTypes")]
    public class JimClaimType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string Type { get; set; } = null!;

        public IList<JimClaim> Claims { get; set; } = new List<JimClaim>();
    }
}