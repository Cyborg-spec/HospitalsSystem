using HospitalSystems.Domain.Appointments;
using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Patients;

namespace HospitalSystems.Domain.Billing;

public class Invoice : AuditableEntity
{
    public decimal Amount { get; private set; }
    public BillingStatus Status { get; private set; }
    public string? PaymentMethod { get; private set; }

    // Foreign keys
    public Guid PatientId { get; private set; }
    public Guid AppointmentId { get; private set; }

    // Navigation properties
    public Patient Patient { get; private set; } = null!;
    public Appointment Appointment { get; private set; } = null!;

    private Invoice() { }

    public Invoice(decimal amount, Guid patientId, Guid appointmentId)
    {
        Amount = amount;
        Status = BillingStatus.Pending;
        PatientId = patientId;
        AppointmentId = appointmentId;
    }
}
