using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Models.Services
{
    public interface IUserStore<TUser>
        where TUser : class, IUserWithClaims
    {
        Task<int> AddUserAsync(TUser user);

        Task<TUser?> GetUserByIdAsync(long id);

        Task<bool> DeleteUserByIdAsync(long id);

        Task<bool> UserWithIdExistsAsync(long id);

        Task<bool> UsernameInUseAsync(string username);

        Task<TUser> GetUserByUsernameAsync(string username);
    }
}
