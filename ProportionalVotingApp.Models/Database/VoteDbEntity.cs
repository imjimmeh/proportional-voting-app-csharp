using ProportionalVotingApp.Models.Base;
using ProportionalVotingApp.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProportionalVotingApp.Models
{
    [Table("Votes")]
    public class VoteDbEntity : IVote<VoteOptionDbEntity>
    {
        public VoteDbEntity()
        {
        }

        public VoteDbEntity(string creator, IEnumerable<string> voteOptions)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            Options = voteOptions.Select(option => new VoteOptionDbEntity(option, this)).ToList();
        }


        public VoteDbEntity(string creator, IList<VoteOptionDbEntity> voteOptions)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            Options = voteOptions ?? throw new ArgumentNullException(nameof(voteOptions));
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; init; }

        public IList<VoteOptionDbEntity> Options { get; init; } = new List<VoteOptionDbEntity>();

        public DateTime CreatedAt { get; init; }

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string Creator { get; set; }

        public bool Completed { get; private set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        public void AddOption(string option)
        {
            Options.Add(new VoteOptionDbEntity(option, this));
        }

        public void SetComplete(bool complete) => Completed = complete;
    }
}