using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Billing;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<IReadOnlyList<Invoice>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Invoice>> GetUnpaidAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Invoice>> GetByStatusAsync(BillingStatus status, CancellationToken cancellationToken = default);
}
