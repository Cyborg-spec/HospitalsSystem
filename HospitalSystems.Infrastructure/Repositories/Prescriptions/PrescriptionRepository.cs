using System.Collections.Immutable;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Prescriptions;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Prescriptions;

public class PrescriptionRepository(ApplicationDbContext dbContext)
    : BaseRepository<Prescription>(dbContext), IPrescriptionRepository
{
    public async Task<IImmutableList<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(p => p.MedicalRecordId == medicalRecordId).ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<IImmutableList<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(p => p.Status == status).ToListAsync(cancellationToken)).ToImmutableList();
    }
}