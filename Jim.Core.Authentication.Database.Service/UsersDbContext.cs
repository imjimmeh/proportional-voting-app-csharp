using Jim.Core.Authentication.Models.Database;
using Jim.Core.Authentication.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Jim.Core.Authentication.Database.Service
{
    public class UsersDbContext : DbContext, IUserStore<User>
    {
        private const string SCHEMA = "users";

        private DbSet<User> _users { get; set; } = null!;

        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        protected UsersDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasKey(userClaim => new { userClaim.UserId, userClaim.ClaimId });
            });
        }

        public async Task<int> AddUserAsync(User user)
        {
            await _users.AddAsync(user);
            var amountAdded = await SaveChangesAsync();

            return amountAdded;
        }

        public async Task<User?> GetUserByIdAsync(long id)
            => await _users.Where(user => user.Id == id)
                        .Select(user => user)
                        .FirstOrDefaultAsync();

        public async Task<bool> DeleteUserByIdAsync(long id)
        {
            var userExists = await UserWithIdExistsAsync(id);
            if (userExists)
            {
                var fauxUser = new User { Id = id };
                _users.RemoveRange(fauxUser);

                var amountRemoved = await SaveChangesAsync();

                return amountRemoved == 1 ? true : amountRemoved == 0 ?
                    throw new Exception($"Found matching User with Id {id} but failed to remove any entities") :
                    throw new Exception($"Somehow removed {amountRemoved} entities, even though should have filtered by PK ID with value {id}");
            }

            throw new KeyNotFoundException($"Unable to find user with id {id}");
        }

        public Task<bool> UserWithIdExistsAsync(long id) => _users.AnyAsync(user => user.Id == id);

        public Task<bool> UsernameInUseAsync(string username) => _users.AnyAsync(user => user.Username == username);

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return null;

                var matchingUser = await _users.Where(user => user.Username == username)
                                               .FirstOrDefaultAsync();

                return matchingUser;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error finding user {username}", ex.Message);
            }
        }
    }
}