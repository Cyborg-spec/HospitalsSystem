using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.AssignRoleToUser;

public class AssignRoleToUserCommandHandler(IRoleService roleService)
    : IRequestHandler<AssignRoleToUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        return await roleService.AssignRoleToUserAsync(request.UserId, request.RoleName);
    }
}
