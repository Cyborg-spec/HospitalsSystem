using HospitalSystems.Application.Users.Queries.Shared;
using MediatR;

namespace HospitalSystems.Application.Users.Queries.GetUsers;

public record GetUsersQuery(
    Guid? HospitalId = null,
    Guid? DepartmentId = null) : IRequest<List<UserDto>>;
