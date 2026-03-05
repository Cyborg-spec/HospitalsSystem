using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Hospitals;

namespace HospitalSystems.Domain.Users;

public class LabTechnician : AuditableEntity
{
    public string? Specialization { get; private set; }

    // Foreign keys
    public Guid UserId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid HospitalId { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Department Department { get; private set; } = null!;
    public Hospital Hospital { get; private set; } = null!;

    private LabTechnician() { }

    public LabTechnician(Guid userId, Guid departmentId, Guid hospitalId, string? specialization)
    {
        UserId = userId;
        DepartmentId = departmentId;
        HospitalId = hospitalId;
        Specialization = specialization;
    }
}