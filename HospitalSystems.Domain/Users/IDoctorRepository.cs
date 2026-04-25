using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Users;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<IImmutableList<Doctor>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default);
    Task<IImmutableList<Doctor>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default);
    Task<IImmutableList<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}