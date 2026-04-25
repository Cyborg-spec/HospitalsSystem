using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Common.Interfaces;

public interface IRoleService
{
    Task<IdentityResult> CreateRoleAsync(string roleName, List<string> permissions);
    Task<IdentityResult> DeleteRoleAsync(string roleName);
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<List<string>> GetRolePermissionsAsync(string roleName);
    Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission);
    Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission);
    Task<IdentityResult> AssignRoleToUserAsync(Guid userId, string roleName);
    Task<IdentityResult> RemoveRoleFromUserAsync(Guid userId, string roleName);
    Task<List<string>> GetUserRolesAsync(Guid userId);
}

public record RoleDto(Guid Id, string Name, List<string> Permissions);
