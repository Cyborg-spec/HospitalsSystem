using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.MedicalRecords;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<List<MedicalRecord>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}
