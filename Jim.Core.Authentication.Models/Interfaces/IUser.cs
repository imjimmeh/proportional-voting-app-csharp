using Jim.Core.Database;

namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IUser
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }

    public interface IDatabaseUser : IUser, IDatabaseEntity
    {
    }
}
