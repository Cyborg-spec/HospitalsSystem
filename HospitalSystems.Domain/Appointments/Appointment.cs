using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Patients;
using HospitalSystems.Domain.Users;

namespace HospitalSystems.Domain.Appointments;

public class Appointment : AuditableEntity
{
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public string? Type { get; private set; }
    public string? Notes { get; private set; }

    // Foreign keys
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }

    // Navigation properties
    public Patient Patient { get; private set; } = null!;
    public Doctor Doctor { get; private set; } = null!;

    private Appointment() { }

    public Appointment(DateTime startTime, DateTime endTime, Guid patientId, Guid doctorId, string? type, string? notes)
    {
        if (startTime >= endTime)
            throw new ArgumentException("StartTime must be before EndTime.");

        StartTime = startTime;
        EndTime = endTime;
        Status = AppointmentStatus.Scheduled;
        PatientId = patientId;
        DoctorId = doctorId;
        Type = type;
        Notes = notes;
    }
}