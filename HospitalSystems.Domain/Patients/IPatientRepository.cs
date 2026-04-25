using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Patients;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<IImmutableList<Patient>> SearchAsync(string searchTerm, int page, int pageSize, CancellationToken cancellationToken = default);
}