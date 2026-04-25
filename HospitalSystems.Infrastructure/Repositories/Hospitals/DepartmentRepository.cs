using System.Collections.Immutable;
using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Hospitals;

public class DepartmentRepository(ApplicationDbContext dbContext)
    : BaseRepository<Department>(dbContext), IDepartmentRepository
{
    public async Task<IImmutableList<Department>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(x => x.HospitalId == hospitalId).ToListAsync(cancellationToken)).ToImmutableList();
    }
}