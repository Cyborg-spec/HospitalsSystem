using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Hospitals;

public class HospitalRepository(ApplicationDbContext dbContext)
    : BaseRepository<Hospital>(dbContext), IHospitalRepository
{
    public async Task<Hospital?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<Hospital?> GetWithDepartmentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(h => h.Departments)
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }
}