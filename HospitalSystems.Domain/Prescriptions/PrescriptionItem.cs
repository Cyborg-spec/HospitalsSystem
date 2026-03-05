using HospitalSystems.Domain.Common;

namespace HospitalSystems.Domain.Prescriptions;

public class PrescriptionItem : BaseEntity
{
    public string MedicationName { get; private set; }
    public string Dosage { get; private set; }
    public string Frequency { get; private set; }
    public string Duration { get; private set; }

    // Foreign keys
    public Guid PrescriptionId { get; private set; }

    // Navigation properties
    public Prescription Prescription { get; private set; } = null!;

    private PrescriptionItem() { }

    public PrescriptionItem(string medicationName, string dosage, string frequency, string duration, Guid prescriptionId)
    {
        MedicationName = medicationName;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        PrescriptionId = prescriptionId;
    }
}