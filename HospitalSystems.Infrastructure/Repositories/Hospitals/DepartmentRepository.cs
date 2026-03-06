using System.Linq.Expressions;
using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Hospitals;

public class DepartmentRepository:IDepartmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.ToListAsync(cancellationToken);
    }

    public async Task<List<Department>> FindAsync(Expression<Func<Department, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Department entity, CancellationToken cancellationToken = default)
    {
       await _dbContext.Departments.AddAsync(entity, cancellationToken);
    }

    public void Update(Department entity)
    {
        _dbContext.Departments.Update(entity);
    }

    public void Delete(Department entity)
    {
        _dbContext.Departments.Remove(entity);
    }

    public async Task<List<Department>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.Where(x => x.HospitalId == hospitalId).ToListAsync(cancellationToken);
    }
}