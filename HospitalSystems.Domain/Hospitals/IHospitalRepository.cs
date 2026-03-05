using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Hospitals;

public interface IHospitalRepository : IRepository<Hospital>
{
    Task<Hospital?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Hospital?> GetWithDepartmentsAsync(Guid id, CancellationToken cancellationToken = default);
}