using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.LabOrders;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.LabOrders;

public class LabOrderRepository(ApplicationDbContext dbContext)
    : BaseRepository<LabOrder>(dbContext), ILabOrderRepository
{
    public async Task<IReadOnlyList<LabOrder>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(l => l.MedicalRecordId == medicalRecordId).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LabOrder>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(l => l.DoctorId == doctorId).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LabOrder>> GetByStatusAsync(LabOrderStatus status, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(l => l.LabOrderStatus == status).ToListAsync(cancellationToken);
    }
}