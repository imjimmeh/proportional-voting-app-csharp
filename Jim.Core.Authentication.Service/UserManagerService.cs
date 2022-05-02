using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Exceptions;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Encryption.Models;
using Jim.Core.Encryption.Service;

namespace Jim.Core.Authentication.Service
{
    public class UserManagerService<TUser> : IUserManagerService where TUser : class, IDatabaseUser, new()
    {
        private IEncryptionService _encryptionService;
        private IUserStore<TUser> _usersStore;

        public UserManagerService(IEncryptionService encryptionService, IUserStore<TUser> usersStore)
        {
            _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
            _usersStore = usersStore ?? throw new ArgumentNullException(nameof(usersStore));
        }

        public async Task<long?> CreateNewUser(CreateUserDTO newUser)
        {
            var validation = await IsValidRequest(newUser);

            if (!validation.isValid)
                throw new InvalidDataException($"User request is not valid - {validation.errorMessage}");

            var userDbEntity = ProjectUser(newUser);

            var encryptedPassword = _encryptionService.HashString(newUser.Password);

            userDbEntity.WithPassword(new HashedPasswordWithSalt(encryptedPassword));

            var countAffected = await _usersStore.AddUserAsync(userDbEntity);

            if (countAffected <= 0)
                throw new Exception($"No user was created");

            return userDbEntity.Id;
        }

        public async Task<GetUserDTO?> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var existingUser = await _usersStore.GetUserByUsernameAsync(loginRequest.Username);

                if (existingUser == null)
                    throw new AuthenticationException(AuthenticationFailureReason.InvalidUsername);

                var encryptedPassword = _encryptionService.VerifyHashedString(loginRequest.Username, new HashedStringWithSalt(existingUser.HashedPassword, existingUser.PasswordSalt));

                if (!encryptedPassword)
                    throw new AuthenticationException(AuthenticationFailureReason.InvalidPassword);

                return new GetUserDTO(existingUser);
            }
            catch(Exception ex)
            {
                throw;
            }
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

            var usernameInUse = await _usersStore.UsernameInUseAsync(newUser.Username);

            if (usernameInUse)
                return new(false, "Username is in use");

            return new(true, null);
        }
    }
}