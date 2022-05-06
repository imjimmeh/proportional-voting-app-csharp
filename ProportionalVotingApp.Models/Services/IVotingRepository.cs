using ProportionalVotingApp.Models.Database;
using ProportionalVotingApp.Models.DTOs;
using ProportionalVotingApp.Models.Requests;
using System.Linq.Expressions;

namespace ProportionalVotingApp.Models.Services
{
    public interface IVotingRepository
    {
        Task<long> AddVoteAsync(VoteDTO vote);
        Task<List<VoteWithIdDTO>> GetVotesAsync(Expression<Func<VoteDbEntity, bool>> predicate);

        Task<VoteWithIdDTO?> GetVoteByIdAsync(long id);

        IQueryable<VoteDbEntity>? AcceptRequest<TRequest>(TRequest request) where TRequest : IVoteRequest;
    }
}