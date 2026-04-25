using HospitalSystems.Domain.Enums;
using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role,
    Guid? HospitalId = null,
    Guid? DepartmentId = null) : IRequest<bool>;
