using ProportionalVotingApp.Models.Base;
using ProportionalVotingApp.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace ProportionalVotingApp.Models.DTOs
{
    public class VoteDTO : IVote<VoteOptionDTO>
    {
        public IList<VoteOptionDTO> Options { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string Creator { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Completed { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        public VoteDTO()
        {
            Options = new List<VoteOptionDTO>();
            Creator = "";
            CreatedAt = DateTime.UtcNow;
            Completed = false;
        }

        public VoteDTO(IList<VoteOptionDTO> options, string creator)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Creator = creator;
            CreatedAt = DateTime.UtcNow;
            Completed = false;
        }

        public VoteDTO(VoteDbEntity dbEntity)
        {
            if (dbEntity == null)
                throw new ArgumentNullException(nameof(dbEntity));

            Creator = dbEntity.Creator;
            CreatedAt = dbEntity.CreatedAt;
            Options = dbEntity.Options.Select(option => new VoteOptionDTO(option.Id, option.Value)).ToList();
            Name = dbEntity.Name;
        }

        public void AddOption(VoteOptionDTO option)
        {
            Options.Add(option);
        }

    }
}
