using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProportionalVotingApp.Models.Database
{
    [Table("VoteOptions")]
    public class VoteOptionDbEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        [MinLength(1)]
        public string Value { get; set; } = null!;

        [Range(1, long.MaxValue)]
        public long VoteId { get; set; }

        public VoteDbEntity Vote { get; set; } = null!;

        public VoteOptionDbEntity()
        {
        }

        public VoteOptionDbEntity(string value, VoteDbEntity vote)
        {
            ValidateAndSetValue(value);
            Vote = vote;
        }

        public VoteOptionDbEntity(string value, long voteId)
        {
            ValidateAndSetValue(value);
            ValidateAndSetVoteId(voteId);
        }


        private void ValidateAndSetValue(string value)
        {
            Value = ValueIsValid(value) ? value : throw new ArgumentNullException(nameof(value));
        }

        private static bool ValueIsValid(string value) => !string.IsNullOrEmpty(value);

        private void ValidateAndSetVoteId(long voteId)
        {
            VoteId = VoteIdIsValid(voteId);
        }

        private static long VoteIdIsValid(long voteId)
        {
            return voteId > 0 ? voteId : throw new ArgumentNullException(nameof(voteId));
        }
    }
}
