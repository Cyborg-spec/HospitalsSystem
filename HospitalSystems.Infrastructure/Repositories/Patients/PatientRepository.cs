using System.Linq.Expressions;
using HospitalSystems.Domain.Patients;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Patients;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.ToListAsync(cancellationToken);
    }

    public async Task<List<Patient>> FindAsync(Expression<Func<Patient, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Patient entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Patients.AddAsync(entity, cancellationToken);
    }

    public void Update(Patient entity)
    {
        _dbContext.Patients.Update(entity);
    }

    public void Delete(Patient entity)
    {
        _dbContext.Patients.Remove(entity);
    }

    public async Task<Patient?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.FirstOrDefaultAsync(p => p.NationalId == nationalId, cancellationToken);
    }

    public async Task<bool> ExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.AnyAsync(p => p.NationalId == nationalId, cancellationToken);
    }

    public async Task<List<Patient>> SearchAsync(string searchTerm, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Patients.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm));
        }

        query = query.OrderBy(p => p.LastName);
        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return result;
    }
}