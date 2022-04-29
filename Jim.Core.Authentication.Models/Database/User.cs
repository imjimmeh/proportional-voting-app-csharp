using Jim.Core.Authentication.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jim.Core.Authentication.Models.Database
{
    [Table("Users")]
    public class User : IDatabaseUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}