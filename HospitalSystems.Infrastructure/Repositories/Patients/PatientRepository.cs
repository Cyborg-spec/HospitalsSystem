using System.Collections.Immutable;
using HospitalSystems.Domain.Patients;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Patients;

public class PatientRepository(ApplicationDbContext dbContext)
    : BaseRepository<Patient>(dbContext), IPatientRepository
{
    public async Task<Patient?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.NationalId == nationalId, cancellationToken);
    }

    public async Task<bool> ExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(p => p.NationalId == nationalId, cancellationToken);
    }

    public async Task<IImmutableList<Patient>> SearchAsync(string searchTerm, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm));
        }

        query = query.OrderBy(p => p.LastName);
        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return result.ToImmutableList();
    }
}