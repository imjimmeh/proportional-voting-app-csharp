using Microsoft.EntityFrameworkCore;
using ProportionalVotingApp.Models.Database;
using ProportionalVotingApp.Models.DTOs;
using ProportionalVotingApp.Models.Services;
using System.Linq.Expressions;

namespace ProportionalVotingApp.DatabaseService
{
    public class VotingRepository : DbContext, IVotingRepository
    {
        private const string SCHEMA = "votes";

        private DbSet<VoteDbEntity> _votes { get; set; } = null!;

        public VotingRepository(DbContextOptions options) : base(options)
        {
        }

        protected VotingRepository()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<long> AddVoteAsync(VoteDTO vote)
        {
            var entity = new VoteDbEntity
            {
                Creator = vote.Creator,
                CreatedAt = vote.CreatedAt,
                Options = vote.Options.Select(option => new VoteOptionDbEntity
                {
                    Value = option.Value
                }).ToList(),
                Name = vote.Name
            };
            await _votes.AddAsync(entity);
            var result = await SaveChangesAsync();

            if (result <= 0 || entity.Id == 0)
                throw new Exception("Error creating Vote - no entities added");

            return entity.Id;
        }

        public Task<List<VoteWithIdDTO>> GetVotesAsync(Expression<Func<VoteDbEntity, bool>> predicate)
        {
            var quotesMatchingPredicate = _votes.Where(predicate);

            var projectedToDto = quotesMatchingPredicate.Select(ConvertToDTOWithId);

            return projectedToDto.ToListAsync();
        }

        private static Expression<Func<VoteDbEntity, VoteWithIdDTO>> ConvertToDTOWithId => vote => new VoteWithIdDTO
            {
                Id = vote.Id,
                Completed = vote.Completed,
                CreatedAt = vote.CreatedAt,
                Options = vote.Options.Select(option => new VoteOptionDTO
                {
                    Id = option.Id,
                    Value = option.Value
                }).ToList(),
                Creator = vote.Creator,
                Name = vote.Name
            };

        public Task<VoteWithIdDTO?> GetVoteByIdAsync(long id)
        {
            return _votes.Where(vote => vote.Id == id).Select(ConvertToDTOWithId).FirstOrDefaultAsync();
        }
    }
}