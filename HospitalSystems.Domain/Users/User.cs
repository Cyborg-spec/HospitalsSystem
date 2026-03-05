using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Hospitals;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Domain.Users;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid? HospitalId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public Hospital? Hospital { get; private set; }
    public Department? Department { get; private set; }

    private User() { }

    public User(string firstName, string lastName, string email, UserRole role, Guid? hospitalId, Guid? departmentId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = email;
        Role = role;
        HospitalId = hospitalId;
        DepartmentId = departmentId;
        IsActive = true;
    }
}