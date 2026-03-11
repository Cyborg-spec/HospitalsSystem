using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Hospitals;

public class DepartmentRepository(ApplicationDbContext dbContext)
    : BaseRepository<Department>(dbContext), IDepartmentRepository
{
    public async Task<IReadOnlyList<Department>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(x => x.HospitalId == hospitalId).ToListAsync(cancellationToken);
    }
}