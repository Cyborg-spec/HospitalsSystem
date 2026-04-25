using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.DeleteRole;

public class DeleteRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<DeleteRoleCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.DeleteRoleAsync(request.RoleName);
    }
}
