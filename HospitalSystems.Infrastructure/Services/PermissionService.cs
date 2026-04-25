using HospitalSystems.Application.Common.Interfaces;
using HospitalSystems.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalSystems.Infrastructure.Services;

public class PermissionService(UserManager<User> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IMemoryCache memoryCache):IPermissionService
{
    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
    {
        var cacheKey = $"permissions_{userId}";
        if (memoryCache.TryGetValue(cacheKey, out HashSet<string>? cachedPermissions) && cachedPermissions != null)
        {
            return cachedPermissions;
        }
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return new HashSet<string>();
        var permissions = new HashSet<string>();
        var roles = await userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                foreach (var claim in roleClaims.Where(c => c.Type == "Permission"))
                {
                    permissions.Add(claim.Value);
                }
            }

        }
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
        memoryCache.Set(cacheKey, permissions, cacheOptions);
        return permissions;
    }
}
    