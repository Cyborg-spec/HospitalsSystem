using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace HospitalSystems.Infrastructure.Auth;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if (policy != null) return policy;
        if (policyName.StartsWith("Permissions", StringComparison.OrdinalIgnoreCase))
        {
            // Dynamically build a policy requiring that specific claim!
            return new AuthorizationPolicyBuilder()
                .RequireClaim("Permission", policyName)
                .Build();
        }
        return null;
    }
}