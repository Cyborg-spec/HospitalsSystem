using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Hospitals;

namespace HospitalSystems.Domain.Users;

public class Nurse : AuditableEntity
{
    public string ShiftType { get; private set; }

    // Foreign keys
    public Guid UserId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid HospitalId { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Department Department { get; private set; } = null!;
    public Hospital Hospital { get; private set; } = null!;

    private Nurse() { }

    public Nurse(string shiftType, Guid userId, Guid departmentId, Guid hospitalId)
    {
        ShiftType = shiftType;
        UserId = userId;
        DepartmentId = departmentId;
        HospitalId = hospitalId;
    }
}