using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Application.Users.Queries;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role,
    Guid? HospitalId,
    Guid? DepartmentId,
    bool IsActive);
