using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Prescriptions;

public interface IPrescriptionRepository : IRepository<Prescription>
{
    Task<List<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default);
    Task<List<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken cancellationToken = default);
}