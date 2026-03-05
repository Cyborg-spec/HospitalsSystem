using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Hospitals;

namespace HospitalSystems.Domain.Users;

public class Doctor : AuditableEntity
{
    public string Specialization { get; private set; }
    public string LicenseNumber { get; private set; }

    // Foreign keys
    public Guid UserId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid HospitalId { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Department Department { get; private set; } = null!;
    public Hospital Hospital { get; private set; } = null!;

    private Doctor() { }

    public Doctor(string specialization, string licenseNumber, Guid userId, Guid departmentId, Guid hospitalId)
    {
        Specialization = specialization;
        LicenseNumber = licenseNumber;
        UserId = userId;
        DepartmentId = departmentId;
        HospitalId = hospitalId;
    }
}