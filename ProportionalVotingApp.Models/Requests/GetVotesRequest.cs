using ProportionalVotingApp.Models.DTOs;
using ProportionalVotingApp.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProportionalVotingApp.Models.Requests
{
    public record GetVotesRequest : IVoteRequest
    {
        private readonly int _take;
        private readonly int _skip;
        private readonly long _minVoteId;

        public GetVotesRequest(int take, int skip, long minVoteId)
        {
            _take = take;
            _skip = skip;
            _minVoteId = minVoteId;

            (var validValues, string[]? errors) = ValuesValid;

            if (!validValues)
                throw new ArgumentNullException(errors != null ? string.Join(",", errors) : "Invalid values supplied");
        }

        public (bool valid, string[]? errors) ValuesValid => _take > 0 ? (true, null) : (false, new[] { "Take must be greater than 0" });

        public Task Accept(IVotingRepository db)
        {
            throw new NotImplementedException();
        }

        public IQueryable<VoteWithIdDTO> Visit(IVotingRepository db)
        {
            var votes = db.AcceptRequest(this);

            if (votes == null)
                throw new Exception("Request was not accepted by database");

            return votes.Where(vote => vote.Id > _minVoteId).OrderBy(vote => vote.Id).Skip(_skip).Take(_take).Select(vote => new VoteWithIdDTO(vote));
        }
    }
}
