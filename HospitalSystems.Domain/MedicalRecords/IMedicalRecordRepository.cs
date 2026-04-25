using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.MedicalRecords;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<IImmutableList<MedicalRecord>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}
