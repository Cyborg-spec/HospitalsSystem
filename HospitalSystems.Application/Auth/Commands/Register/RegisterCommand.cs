using HospitalSystems.Domain.Enums;
using HospitalSystems.Infrastructure.Auth;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid? HospitalId = null,
    Guid? DepartmentId = null) : IRequest<TokenResponse>;
    