using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("ClaimTypes")]
    public class JimClaimType
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public IList<JimClaim> Claims { get; set; } = new List<JimClaim>();
    }
}