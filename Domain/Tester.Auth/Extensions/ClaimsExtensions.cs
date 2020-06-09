using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Tester.Auth.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid GetUserId(this IEnumerable<Claim> claims)
        {
            return Guid.Parse(claims.Single(c => c.Type == "userId").Value);
        }

        public static Guid? GetOrDefaultUserId(this IEnumerable<Claim> claims)
        {
            var userId = claims.SingleOrDefault(c => c.Type == "userId");
            return userId == null ? (Guid?) null : Guid.Parse(userId.Value);
        }
    }
}