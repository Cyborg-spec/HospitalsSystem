using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Hospitals;

namespace HospitalSystems.Domain.Users;

public class Receptionist : AuditableEntity
{
    // Foreign keys
    public Guid UserId { get; private set; }
    public Guid HospitalId { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Hospital Hospital { get; private set; } = null!;

    private Receptionist() { }

    public Receptionist(Guid userId, Guid hospitalId)
    {
        UserId = userId;
        HospitalId = hospitalId;
    }
}