using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProportionalVotingApp.Models.DTOs
{
    public class VoteOptionDTO
    {
        public VoteOptionDTO()
        {
        }

        public VoteOptionDTO(long id, string value)
        {
            Id = id;
            Value = value;
        }

        public long Id { get; init; }

        public string Value { get; set; }
    }
}
