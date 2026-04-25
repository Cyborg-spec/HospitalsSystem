using System.Security.Claims;
using HospitalSystems.Application.Common.Interfaces;
using HospitalSystems.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalSystems.Infrastructure.Services;

public class RoleService(
    RoleManager<IdentityRole<Guid>> roleManager,
    UserManager<User> userManager,
    IMemoryCache memoryCache) : IRoleService
{
    private async Task InvalidateCacheForRoleAsync(string roleName)
    {
        var users = await userManager.GetUsersInRoleAsync(roleName);
        foreach (var user in users)
        {
            memoryCache.Remove($"permissions_{user.Id}");
        }
    }
    public async Task<IdentityResult> CreateRoleAsync(string roleName, List<string> permissions)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (roleExists)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleAlreadyExists",
                Description = $"Role '{roleName}' already exists."
            });
        }

        var role = new IdentityRole<Guid>(roleName);
        var result = await roleManager.CreateAsync(role);
        if (!result.Succeeded) return result;

        // Add each permission as a claim on this role
        foreach (var permission in permissions)
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        }

        return result;
    }

    public async Task<IdentityResult> DeleteRoleAsync(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"Role '{roleName}' was not found."
            });
        }

        var usersToInvalidate = await userManager.GetUsersInRoleAsync(roleName);

        var result = await roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            foreach (var user in usersToInvalidate)
            {
                memoryCache.Remove($"permissions_{user.Id}");
            }
        }
        return result;
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var roles = roleManager.Roles.ToList();
        var roleDtos = new List<RoleDto>();

        foreach (var role in roles)
        {
            var claims = await roleManager.GetClaimsAsync(role);
            var permissions = claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            roleDtos.Add(new RoleDto(role.Id, role.Name!, permissions));
        }

        return roleDtos;
    }

    public async Task<List<string>> GetRolePermissionsAsync(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
            throw new Exception($"Role '{roleName}' was not found.");

        var claims = await roleManager.GetClaimsAsync(role);
        return claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();
    }

    public async Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"Role '{roleName}' was not found."
            });
        }

        // Check if permission already exists on this role
        var existingClaims = await roleManager.GetClaimsAsync(role);
        if (existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "PermissionAlreadyExists",
                Description = $"Permission '{permission}' already exists on role '{roleName}'."
            });
        }

        var result = await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        if (result.Succeeded)
        {
            await InvalidateCacheForRoleAsync(roleName);
        }
        return result;
    }

    public async Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"Role '{roleName}' was not found."
            });
        }

        var result = await roleManager.RemoveClaimAsync(role, new Claim("Permission", permission));
        if (result.Succeeded)
        {
            await InvalidateCacheForRoleAsync(roleName);
        }
        return result;
    }

    public async Task<IdentityResult> AssignRoleToUserAsync(Guid userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = $"User with ID '{userId}' was not found."
            });
        }

        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"Role '{roleName}' was not found."
            });
        }

        var result = await userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            memoryCache.Remove($"permissions_{userId}");
        }
        return result;
    }

    public async Task<IdentityResult> RemoveRoleFromUserAsync(Guid userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = $"User with ID '{userId}' was not found."
            });
        }

        var result = await userManager.RemoveFromRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            memoryCache.Remove($"permissions_{userId}");
        }
        return result;
    }

    public async Task<List<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new Exception($"User with ID '{userId}' was not found.");

        var roles = await userManager.GetRolesAsync(user);
        return roles.ToList();
    }
}
