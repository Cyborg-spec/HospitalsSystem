using System.Linq.Expressions;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Prescriptions;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Prescriptions;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PrescriptionRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Prescriptions.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Prescription>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Prescriptions.ToListAsync(cancellationToken);
    }

    public async Task<List<Prescription>> FindAsync(Expression<Func<Prescription, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Prescriptions.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Prescriptions.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Prescription entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Prescriptions.AddAsync(entity, cancellationToken);
    }

    public void Update(Prescription entity)
    {
        _dbContext.Prescriptions.Update(entity);
    }

    public void Delete(Prescription entity)
    {
        _dbContext.Prescriptions.Remove(entity);
    }

    public async Task<List<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Prescriptions.Where(p => p.MedicalRecordId == medicalRecordId).ToListAsync(cancellationToken);
    }

    public async Task<List<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Prescriptions.Where(p => p.Status == status).ToListAsync(cancellationToken);
    }
}