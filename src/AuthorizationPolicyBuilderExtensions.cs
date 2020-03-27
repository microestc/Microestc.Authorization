using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Microestc.Authorization
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireLicence(this AuthorizationPolicyBuilder policyBuilder, string licenceKey)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            policyBuilder.RequireAuthenticatedUser();
            policyBuilder.Requirements.Add(new LicenceAuthorizationRequirement(licenceKey));
            return policyBuilder;
        }

        public static AuthorizationPolicyBuilder RequireLicences(this AuthorizationPolicyBuilder policyBuilder, params string[] licenceKeys)
        {
            if (licenceKeys == null) throw new ArgumentNullException(nameof(licenceKeys));
            policyBuilder.RequireAuthenticatedUser();
            policyBuilder.Requirements.Add(new LicenceAuthorizationRequirement(licenceKeys));
            return policyBuilder;
        }

        public static AuthorizationPolicyBuilder RequireSA(this AuthorizationPolicyBuilder policyBuilder)
        {
            policyBuilder.RequireAuthenticatedUser();
            policyBuilder.Requirements.Add(new LicenceAuthorizationRequirement(LicenceKeys.SA));
            return policyBuilder;
        }

    }
}

