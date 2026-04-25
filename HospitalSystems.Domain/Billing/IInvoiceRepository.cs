using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Billing;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<IImmutableList<Invoice>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IImmutableList<Invoice>> GetUnpaidAsync(CancellationToken cancellationToken = default);
    Task<IImmutableList<Invoice>> GetByStatusAsync(BillingStatus status, CancellationToken cancellationToken = default);
}
