using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Hospitals;

namespace HospitalSystems.Domain.Users;

public class Pharmacist : AuditableEntity
{
    public string LicenseNumber { get; private set; }

    // Foreign keys
    public Guid UserId { get; private set; }
    public Guid HospitalId { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Hospital Hospital { get; private set; } = null!;

    private Pharmacist() { }

    public Pharmacist(string licenseNumber, Guid userId, Guid hospitalId)
    {
        LicenseNumber = licenseNumber;
        UserId = userId;
        HospitalId = hospitalId;
    }
}