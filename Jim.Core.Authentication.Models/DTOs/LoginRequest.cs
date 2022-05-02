using System.ComponentModel.DataAnnotations;

namespace Jim.Core.Authentication.Models.DTOs
{
    public record LoginRequest
    {
        [Required]
        [MinLength(5)]
        public string Username { get; init; } = null!;

        [Required]
        public string Password { get; init; } = null!;
    }
}
