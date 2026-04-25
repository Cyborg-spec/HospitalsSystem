namespace HospitalSystems.Application.Users.Queries.Shared;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    Guid? HospitalId,
    Guid? DepartmentId,
    bool IsActive);
