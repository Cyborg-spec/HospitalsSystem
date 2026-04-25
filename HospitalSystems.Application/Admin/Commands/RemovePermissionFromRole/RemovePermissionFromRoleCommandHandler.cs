using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.RemovePermissionFromRole;

public class RemovePermissionFromRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<RemovePermissionFromRoleCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(RemovePermissionFromRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.RemovePermissionFromRoleAsync(request.RoleName, request.Permission);
    }
}
