using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProportionalVotingApp.Models.DTOs
{
    [Table("Users")]
    public class VoteUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(30)]
        [MinLength(3)]
        public string Username { get; set; } = "";

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Enabled { get; set; } = false;

        [Required]
        public string HashedPassword { get; set; } = "";
    }
}
