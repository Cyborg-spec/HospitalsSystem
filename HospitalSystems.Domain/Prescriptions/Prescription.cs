using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.MedicalRecords;

namespace HospitalSystems.Domain.Prescriptions;

public class Prescription : AuditableEntity
{
    public PrescriptionStatus Status { get; private set; }

    // Foreign keys
    public Guid MedicalRecordId { get; private set; }

    // Navigation properties
    public MedicalRecord MedicalRecord { get; private set; } = null!;
    public ICollection<PrescriptionItem> Items { get; private set; } = new List<PrescriptionItem>();

    private Prescription() { }

    public Prescription(Guid medicalRecordId)
    {
        MedicalRecordId = medicalRecordId;
        Status = PrescriptionStatus.Active;
    }
}