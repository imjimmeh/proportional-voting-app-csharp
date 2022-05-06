using Jim.Core.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProportionalVotingApp.Models.DTOs
{
    public record GetVotesResponse : ApiResponse
    {
        public GetVotesResponse()
        {
        }

        public GetVotesResponse(IList<VoteWithIdDTO> votes) : base(true)
        {
            Votes = votes;
        }

        public GetVotesResponse(IEnumerable<ResponseError> errorMessages) : base(errorMessages)
        {
        }

        public IList<VoteWithIdDTO> Votes { get; init; }
    }
}
