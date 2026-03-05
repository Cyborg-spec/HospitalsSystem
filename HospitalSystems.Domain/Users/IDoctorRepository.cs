using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Users;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<List<Doctor>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}