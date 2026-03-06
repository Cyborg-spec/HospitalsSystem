using System.Linq.Expressions;
using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Users;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DoctorRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.ToListAsync(cancellationToken);
    }

    public async Task<List<Doctor>> FindAsync(Expression<Func<Doctor, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.AnyAsync(d => d.Id == id, cancellationToken);
    }

    public async Task AddAsync(Doctor entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Doctors.AddAsync(entity, cancellationToken);
    }

    public void Update(Doctor entity)
    {
        _dbContext.Doctors.Update(entity);
    }

    public void Delete(Doctor entity)
    {
        _dbContext.Doctors.Remove(entity);
    }

    public async Task<List<Doctor>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.Where(d => d.DepartmentId == departmentId).ToListAsync(cancellationToken);
    }

    public async Task<List<Doctor>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default)
    {
        // Notice: Doctor might not directly map to HospitalId, but via Department.
        // E.g. d.Department.HospitalId == hospitalId
        // We will do a generic join or just use navigation properties.
        return await _dbContext.Doctors
            .Include(d => d.Department)
            .Where(d => d.Department.HospitalId == hospitalId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.Where(d => d.Specialization == specialization).ToListAsync(cancellationToken);
    }

    public async Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
    }
}