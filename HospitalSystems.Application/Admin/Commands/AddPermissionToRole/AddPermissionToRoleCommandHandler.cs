using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.AddPermissionToRole;

public class AddPermissionToRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<AddPermissionToRoleCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.AddPermissionToRoleAsync(request.RoleName, request.Permission);
    }
}
