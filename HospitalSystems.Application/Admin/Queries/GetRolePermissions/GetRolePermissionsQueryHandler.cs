using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Admin.Queries.GetRolePermissions;

public class GetRolePermissionsQueryHandler(IRoleService roleService)
    : IRequestHandler<GetRolePermissionsQuery, List<string>>
{
    public async Task<List<string>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetRolePermissionsAsync(request.RoleName);
    }
}
