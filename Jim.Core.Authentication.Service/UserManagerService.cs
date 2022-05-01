using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;

namespace Jim.Core.Authentication.Service
{
    public class UserManagerService<TUser> : IUserManagerService where TUser : class, IUserWithClaims, new()
    {
        private IUserStore<TUser> _usersStore;

        public UserManagerService(IUserStore<TUser> usersStore)
        {
            _usersStore = usersStore ?? throw new ArgumentNullException(nameof(usersStore));
        }

        public async Task<long?> CreateNewUser(CreateUserDTO newUser)
        {
            var validation = await IsValidRequest(newUser);

            if (!validation.isValid)
                throw new InvalidDataException($"User request is not valid - {validation.errorMessage}");

            var userDbEntity = ProjectUser(newUser);

            var countAffected = await _usersStore.AddUserAsync(userDbEntity);

            if (countAffected <= 0)
                throw new Exception($"No user was created");

            return userDbEntity.Id;
        }

        private static TUser ProjectUser(CreateUserDTO newUser)
            => new TUser
            {
                Username = newUser.Username,
               // HashedPassword = newUser.Password
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