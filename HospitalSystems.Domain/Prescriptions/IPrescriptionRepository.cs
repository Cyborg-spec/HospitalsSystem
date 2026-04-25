using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Prescriptions;

public interface IPrescriptionRepository : IRepository<Prescription>
{
    Task<IImmutableList<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default);
    Task<IImmutableList<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken cancellationToken = default);
}