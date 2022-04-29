using Jim.Core.Database.Models;

namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IUserWithClaims : IUser, IDatabaseEntity
    {
        public IEnumerable<IClaim> Claims { get; }
    }
}
