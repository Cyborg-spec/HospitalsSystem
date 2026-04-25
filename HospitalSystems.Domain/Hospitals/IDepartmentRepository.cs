using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Hospitals;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<IImmutableList<Department>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default);
}