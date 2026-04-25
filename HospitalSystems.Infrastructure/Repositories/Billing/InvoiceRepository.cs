using System.Collections.Immutable;
using HospitalSystems.Domain.Billing;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Billing;

public class InvoiceRepository(ApplicationDbContext dbContext)
    : BaseRepository<Invoice>(dbContext), IInvoiceRepository
{
    public async Task<IImmutableList<Invoice>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(i => i.PatientId == patientId).ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<IImmutableList<Invoice>> GetUnpaidAsync(CancellationToken cancellationToken = default)
    {
        return (await DbSet
            .Where(i => i.Status == BillingStatus.Pending || i.Status == BillingStatus.PartiallyPaid)
            .ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<IImmutableList<Invoice>> GetByStatusAsync(BillingStatus status, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(i => i.Status == status).ToListAsync(cancellationToken)).ToImmutableList();
    }
}