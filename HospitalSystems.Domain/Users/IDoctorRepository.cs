using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Users;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<IReadOnlyList<Doctor>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}