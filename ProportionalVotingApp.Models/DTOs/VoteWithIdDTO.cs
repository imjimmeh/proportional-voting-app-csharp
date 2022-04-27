using ProportionalVotingApp.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace ProportionalVotingApp.Models.DTOs
{
    public class VoteWithIdDTO : VoteDTO
    {
        public long Id { get; init; }

        public VoteWithIdDTO()
        {
        }

        public VoteWithIdDTO(long id, IList<VoteOptionDTO> options, DateTime createdAt, string creator) : base(options, creator)
        {
            Id = id;
            Options = options ?? new List<VoteOptionDTO>();
            CreatedAt = createdAt;
            Creator = creator;

        }


    }
}