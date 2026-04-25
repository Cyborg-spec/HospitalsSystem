using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.CreateRole;

public class CreateRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<CreateRoleCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return await roleService.CreateRoleAsync(request.Name, request.Permissions);
    }
}
