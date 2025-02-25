using System.Security.Claims;
using PropelAuth.Api.Models;

namespace PropelAuth.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Returns a new <see cref="PropelAuthUser"/> instance based on the current set of claims.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns></returns>
        public static PropelAuthUser GetPropelUser(this ClaimsPrincipal claimsPrincipal) {
            return new PropelAuthUser(claimsPrincipal);
        }
    }
}
