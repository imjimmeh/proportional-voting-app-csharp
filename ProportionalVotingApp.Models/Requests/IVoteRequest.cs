using ProportionalVotingApp.Models.Services;

namespace ProportionalVotingApp.Models.Requests
{
    public interface IVoteRequest
    {
        public Task Accept(IVotingRepository db);
    }
}