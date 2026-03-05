using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Hospitals;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<List<Department>> GetByHospitalIdAsync(Guid hospitalId, CancellationToken cancellationToken = default);
}