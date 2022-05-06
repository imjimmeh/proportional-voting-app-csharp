using ProportionalVotingApp.Models.Base;
using ProportionalVotingApp.Models.Database;
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

        public VoteWithIdDTO(VoteDbEntity dbEntity) : base(dbEntity)
        {
            Id = dbEntity.Id;
        }


    }
}