using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Billing;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<List<Invoice>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetUnpaidAsync(CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetByStatusAsync(BillingStatus status, CancellationToken cancellationToken = default);
}
