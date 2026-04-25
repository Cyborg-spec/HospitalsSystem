using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.RemoveRoleFromUser;

public class RemoveRoleFromUserCommandHandler(IRoleService roleService)
    : IRequestHandler<RemoveRoleFromUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        return await roleService.RemoveRoleFromUserAsync(request.UserId, request.RoleName);
    }
}
