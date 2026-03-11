using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Prescriptions;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Prescriptions;

public class PrescriptionRepository(ApplicationDbContext dbContext)
    : BaseRepository<Prescription>(dbContext), IPrescriptionRepository
{
    public async Task<IReadOnlyList<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(p => p.MedicalRecordId == medicalRecordId).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(p => p.Status == status).ToListAsync(cancellationToken);
    }
}