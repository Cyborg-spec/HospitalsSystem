using HospitalSystems.Domain.Users;
using MediatR;

namespace HospitalSystems.Application.Users.Queries;

public record GetUsersQuery(
    Guid? HospitalId = null,
    Guid? DepartmentId = null) : IRequest<List<UserDto>>;
