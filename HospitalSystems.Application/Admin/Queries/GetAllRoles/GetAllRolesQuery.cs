using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Admin.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<List<RoleDto>>;
