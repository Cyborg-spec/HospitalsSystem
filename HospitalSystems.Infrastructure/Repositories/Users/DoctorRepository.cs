using System.Collections.Immutable;
using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Users;

public class DoctorRepository(ApplicationDbContext dbContext)
    : BaseRepository<Doctor>(dbContext), IDoctorRepository
{
    public async Task<IImmutableList<Doctor>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(d => d.DepartmentId == departmentId).ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<IImmutableList<Doctor>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default)
    {
        return (await DbSet
            .Include(d => d.Department)
            .Where(d => d.Department.HospitalId == hospitalId)
            .ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<IImmutableList<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(d => d.Specialization == specialization).ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
    }
}