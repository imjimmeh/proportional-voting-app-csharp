using ProportionalVotingApp.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace ProportionalVotingApp.Models.DTOs
{
    public class VoteDTO : IVote
    {
        public IList<string> Options { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string Creator { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Completed { get; set; }

        public VoteDTO()
        {
            Options = new List<string>();
            Creator = "";
            CreatedAt = DateTime.UtcNow;
            Completed = false;
        }

        public VoteDTO(IList<string> options, string creator)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Creator = creator;
            CreatedAt = DateTime.UtcNow;
            Completed = false;
        }

        public void AddOption(string option)
        {
            Options.Add(option);
        }

    }
}
