using Jim.Core.Authentication.Models.Database;
using Jim.Core.Authentication.Models.Interfaces;
using System.Security.Claims;

namespace Jim.Core.Authentication.Service
{
    public class ClaimsService
    {
        public static IEnumerable<Claim> ConvertUserClaims(IDatabaseUser user)
        {
            yield return new Claim(ClaimTypes.Name, user.Username);

            foreach (var claim in user.Claims)
                yield return ConvertUserClaim(claim);
        }

        private static Claim ConvertUserClaim(IClaim claim)
            => new Claim(claim.ClaimTypeValue, claim.Value);
    }
}
