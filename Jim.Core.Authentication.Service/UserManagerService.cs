using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;

namespace Jim.Core.Authentication.Service
{
    public class UserManagerService<TUser>
        where TUser : class, IDatabaseUser, new()
    {
        private const string INVALID_USER_ERROR = "User is not valid";

        private IUserStore<TUser> _usersStore;

        public UserManagerService(IUserStore<TUser> usersStore)
        {
            _usersStore = usersStore ?? throw new ArgumentNullException(nameof(usersStore));
        }

        public async Task<bool> CreateNewUser(CreateUserDTO newUser)
        {
            var validation = await IsValidRequest(newUser);

            if (!validation.isValid)
                return false;

            var countAffected = await _usersStore.AddUserAsync(ProjectUser(newUser));

            return countAffected > 0;
        }

        private static TUser ProjectUser(CreateUserDTO newUser)
            => new TUser
            {
                Username = newUser.Username,
                Password = newUser.Password
            };

        private async Task<(bool isValid, string? errorMessage)> IsValidRequest(CreateUserDTO newUser)
        {
            if (!newUser.IsValidRequest)
                return new(false, "Required field is missing");

            var usernameInUse = await _usersStore.UsernameInUse(newUser.Username);

            if (usernameInUse)
                return new(false, "Username is in use");

            return new(true, null);
        }
    }
}