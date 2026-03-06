using System.Linq.Expressions;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.LabOrders;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.LabOrders;

public class LabOrderRepository : ILabOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LabOrderRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<LabOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<List<LabOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.ToListAsync(cancellationToken);
    }

    public async Task<List<LabOrder>> FindAsync(Expression<Func<LabOrder, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.AnyAsync(l => l.Id == id, cancellationToken);
    }

    public async Task AddAsync(LabOrder entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.LabOrders.AddAsync(entity, cancellationToken);
    }

    public void Update(LabOrder entity)
    {
        _dbContext.LabOrders.Update(entity);
    }

    public void Delete(LabOrder entity)
    {
        _dbContext.LabOrders.Remove(entity);
    }

    public async Task<List<LabOrder>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.Where(l => l.MedicalRecordId == medicalRecordId).ToListAsync(cancellationToken);
    }

    public async Task<List<LabOrder>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.Where(l => l.DoctorId == doctorId).ToListAsync(cancellationToken);
    }

    public async Task<List<LabOrder>> GetByStatusAsync(LabOrderStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LabOrders.Where(l => l.LabOrderStatus == status).ToListAsync(cancellationToken);
    }
}