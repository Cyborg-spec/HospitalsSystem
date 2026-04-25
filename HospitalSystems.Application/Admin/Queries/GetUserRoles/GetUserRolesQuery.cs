using MediatR;

namespace HospitalSystems.Application.Admin.Queries.GetUserRoles;

public record GetUserRolesQuery(Guid UserId) : IRequest<List<string>>;
