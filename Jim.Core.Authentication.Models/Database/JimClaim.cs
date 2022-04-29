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
        [StringLength(50)]
        public string Value { get; set; } = "";

        [Range(1, long.MaxValue)]
        public long ClaimTypeId { get; set; }

        public JimClaimType ClaimType { get; set; } = null!;

        [NotMapped]
        public string ClaimTypeValue => ClaimType.Type.ToString();
    }
}
