using ProportionalVotingApp.Models.DTOs;
using System.Linq.Expressions;

namespace ProportionalVotingApp.Models.Services
{
    public interface IVotingRepository
    {
        Task<long> AddVoteAsync(VoteDTO vote);
        Task<List<VoteWithIdDTO>> GetVotesAsync(Expression<Func<VoteDbEntity, bool>> predicate);
    }
}