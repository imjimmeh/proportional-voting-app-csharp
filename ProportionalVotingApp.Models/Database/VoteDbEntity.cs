using ProportionalVotingApp.Models.Base;
using ProportionalVotingApp.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProportionalVotingApp.Models
{
    [Table("Votes")]
    public class VoteDbEntity : IVote
    {
        public VoteDbEntity()
        {
        }

        public VoteDbEntity(string creator, IEnumerable<string> voteOptions)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            VoteOptions = voteOptions.Select(option => new VoteOptionDbEntity(option, this)).ToList();
        }


        public VoteDbEntity(string creator, IList<VoteOptionDbEntity> voteOptions)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            VoteOptions = voteOptions ?? throw new ArgumentNullException(nameof(voteOptions));
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; init; }

        public IList<VoteOptionDbEntity> VoteOptions { get; init; } = new List<VoteOptionDbEntity>();

        public DateTime CreatedAt { get; init; }

        public string Creator { get; init; }

        public bool Completed { get; private set; }

        public IList<string> Options => VoteOptions?.Select(option => option.Value).ToList() ?? new List<string>();

        public void AddOption(string option)
        {
            VoteOptions.Add(new VoteOptionDbEntity(option, this));
        }

        public void SetComplete(bool complete) => Completed = complete;
    }
}