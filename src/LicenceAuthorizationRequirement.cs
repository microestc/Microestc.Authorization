using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Microestc.Authorization
{
    public class LicenceAuthorizationRequirement : AuthorizationHandler<LicenceAuthorizationRequirement>, IAuthorizationRequirement
    {
        public LicenceAuthorizationRequirement(string licenceKey)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            _licenceKeys = new List<string>() { licenceKey };
        }
        public LicenceAuthorizationRequirement(IEnumerable<string> licenceKeys)
        {
            if (licenceKeys == null) throw new ArgumentNullException(nameof(licenceKeys));
            if (!licenceKeys.Any()) throw new InvalidOperationException("not license.");
            _licenceKeys = licenceKeys;
        }

        protected readonly IEnumerable<string> _licenceKeys;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LicenceAuthorizationRequirement requirement)
        {
            if (context.User == null) return Task.CompletedTask;
            if (context.User.HasClaim(c => string.Equals(c.Type, ClaimTypes.Licenced, StringComparison.OrdinalIgnoreCase) && string.Equals(c.Value, LicenceKeys.SA, StringComparison.Ordinal)))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            var flag = false;
            if (requirement._licenceKeys != null && requirement._licenceKeys.Any())
                flag = context.User.HasClaim(c => string.Equals(c.Type, ClaimTypes.Licenced, StringComparison.OrdinalIgnoreCase) && requirement._licenceKeys.Contains(c.Value, StringComparer.Ordinal));
            if (flag) context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}

