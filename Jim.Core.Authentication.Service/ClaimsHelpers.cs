using Jim.Core.Authentication.Models.Interfaces;
using System.Security.Claims;

namespace Jim.Core.Authentication.Service
{
    public static class ClaimsHelpers
    {
        public static IEnumerable<Claim> ConvertUserClaims(this IUserWithClaims user)
        {
            yield return new Claim(ClaimTypes.Name, user.Username);

            foreach (var claim in user.Claims)
                yield return ConvertUserClaim(claim);
        }

        private static Claim ConvertUserClaim(this IClaim claim)
            => new Claim(claim.Type, claim.Value);
    }
}
