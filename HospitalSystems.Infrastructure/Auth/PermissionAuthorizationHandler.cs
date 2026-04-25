using System.Security.Claims;
using HospitalSystems.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystems.Infrastructure.Auth;

public class PermissionAuthorizationHandler(IPermissionService permissionService):AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdString = context.User.FindFirstValue(ClaimTypes.NameIdentifier)
                           ?? context.User.FindFirstValue("sub");
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            return;
        }

        var permissions = await permissionService.GetPermissionsAsync(userId);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

    }
}
