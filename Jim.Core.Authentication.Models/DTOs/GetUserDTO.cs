using Jim.Core.Authentication.Models.Interfaces;

namespace Jim.Core.Authentication.Models.DTOs
{
    public struct GetUserDTO : IUserWithClaims
    {
        public GetUserDTO(IUserWithClaims user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            Claims = user.Claims;
            Username = user.Username;
            Id = user.Id;
            CreatedAt = user.CreatedAt;
            LastModified = user.LastModified;
        }

        public IEnumerable<IClaim> Claims { get; init; }

        public string Username { get; set; }

        public long Id { get; init; } 

        public DateTime CreatedAt { get; set; } 
        public DateTime LastModified { get; set; }
    }
}
