using Microsoft.AspNetCore.Authorization;
namespace HospitalSystems.Infrastructure.Auth;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
