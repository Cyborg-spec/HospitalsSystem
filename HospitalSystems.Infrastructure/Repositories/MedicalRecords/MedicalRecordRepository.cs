using System.Linq.Expressions;
using HospitalSystems.Domain.MedicalRecords;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.MedicalRecords;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicalRecordRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MedicalRecords.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<List<MedicalRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.MedicalRecords.ToListAsync(cancellationToken);
    }

    public async Task<List<MedicalRecord>> FindAsync(Expression<Func<MedicalRecord, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MedicalRecords.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MedicalRecords.AnyAsync(m => m.Id == id, cancellationToken);
    }

    public async Task AddAsync(MedicalRecord entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.MedicalRecords.AddAsync(entity, cancellationToken);
    }

    public void Update(MedicalRecord entity)
    {
        _dbContext.MedicalRecords.Update(entity);
    }

    public void Delete(MedicalRecord entity)
    {
        _dbContext.MedicalRecords.Remove(entity);
    }

    public async Task<List<MedicalRecord>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MedicalRecords.Where(m => m.PatientId == patientId).ToListAsync(cancellationToken);
    }

    public async Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MedicalRecords.FirstOrDefaultAsync(m => m.AppointmentId == appointmentId, cancellationToken);
    }
}