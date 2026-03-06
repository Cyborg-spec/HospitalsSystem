using System.Linq.Expressions;
using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Hospitals;

public class HospitalRepository:IHospitalRepository
{
    private readonly ApplicationDbContext _dbContext;

    public HospitalRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    public async Task<Hospital?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await  _dbContext.Hospitals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Hospital>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Hospitals.ToListAsync(cancellationToken);
    }

    public async Task<List<Hospital>> FindAsync(Expression<Func<Hospital, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Hospitals.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Hospitals.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Hospital entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Hospitals.AddAsync(entity, cancellationToken);
    }

    public void Update(Hospital entity)
    {
        _dbContext.Hospitals.Update(entity);
    }

    public void Delete(Hospital entity)
    {
       _dbContext.Hospitals.Remove(entity);
    }

    public async Task<Hospital?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Hospitals.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<Hospital?> GetWithDepartmentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Hospitals
            .Include(h => h.Departments)
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }
}