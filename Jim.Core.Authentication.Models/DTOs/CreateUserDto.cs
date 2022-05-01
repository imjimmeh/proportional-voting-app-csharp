using Jim.Core.Authentication.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jim.Core.Authentication.Models.DTOs
{
    public class CreateUserDTO : IUser
    {
        public CreateUserDTO()
        {
        }

        public CreateUserDTO(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();
            Username = !string.IsNullOrEmpty(username) ? username : throw new ArgumentNullException(nameof(username));
            Password = !string.IsNullOrEmpty(password) ? password : throw new ArgumentNullException(nameof(password));
        }

        [Required]
        [MinLength(5)]
        [StringLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6)]
        [StringLength(100)]
        public string Password { get; set; } = null!;

        public bool IsValidRequest => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    }
}
