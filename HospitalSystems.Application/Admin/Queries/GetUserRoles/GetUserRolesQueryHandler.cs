using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Admin.Queries.GetUserRoles;

public class GetUserRolesQueryHandler(IRoleService roleService)
    : IRequestHandler<GetUserRolesQuery, List<string>>
{
    public async Task<List<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetUserRolesAsync(request.UserId);
    }
}
