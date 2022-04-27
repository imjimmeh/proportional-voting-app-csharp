namespace ProportionalVotingApp.Models.DTOs
{
    public class VoteWithIdDTO : VoteDTO
    {
        public VoteWithIdDTO()
        {
        }

        public VoteWithIdDTO(long id, IList<string> options, string creator) : base(options, creator)
        {
            Id = id;
        }

        public long Id { get; init; }
    }
}