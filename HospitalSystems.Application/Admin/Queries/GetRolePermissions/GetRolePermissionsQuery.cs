using MediatR;

namespace HospitalSystems.Application.Admin.Queries.GetRolePermissions;

public record GetRolePermissionsQuery(string RoleName) : IRequest<List<string>>;
