using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Models.Services
{
    public interface IUserStore<TUser>
        where TUser : class, IDatabaseUser
    {
        Task<int> AddUserAsync(TUser user);

        Task<TUser?> GetUserById(long id);

        Task<bool> DeleteUserById(long id);

        Task<bool> UserWithIdExists(long id);
        Task<bool> UsernameInUse(string username);
    }
}
