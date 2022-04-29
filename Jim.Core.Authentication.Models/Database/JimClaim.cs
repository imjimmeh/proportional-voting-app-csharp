using Jim.Core.Authentication.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("Claims")]
    public class JimClaim : IClaim
    {
        [Range(0, long.MaxValue)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Value { get; set; } = "";

        [Range(1, long.MaxValue)]
        public long JimClaimTypeId { get; set; }

        public JimClaimType JimClaimType { get; set; } = null!;

        [NotMapped]
        public string ClaimType => JimClaimType.Type.ToString();
    }
}
