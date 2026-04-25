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
            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();
        }

        return null;
    }
}
