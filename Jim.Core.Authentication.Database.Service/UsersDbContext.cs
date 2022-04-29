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
        }

        public async Task<int> AddUserAsync(User user)
        {
            await _users.AddAsync(user);
            var amountAdded = await SaveChangesAsync();

            return amountAdded;
        }

        public async Task<User?> GetUserById(long id)
            => await _users.Where(user => user.Id == id)
                        .Select(user => user)
                        .FirstOrDefaultAsync();

        public async Task<bool> DeleteUserById(long id)
        {
            var userExists = await UserWithIdExists(id);
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

        public Task<bool> UserWithIdExists(long id) => _users.AnyAsync(user => user.Id == id);

        public Task<bool> UsernameInUse(string username) => _users.AnyAsync(user => user.Username == username);

    }
}