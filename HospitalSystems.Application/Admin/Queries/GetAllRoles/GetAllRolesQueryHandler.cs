using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Admin.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(IRoleService roleService)
    : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetAllRolesAsync();
    }
}
