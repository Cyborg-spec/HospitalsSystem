using System.Linq.Expressions;
using HospitalSystems.Domain.Billing;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Billing;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public InvoiceRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<List<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices.ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> FindAsync(Expression<Func<Invoice, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices.AnyAsync(i => i.Id == id, cancellationToken);
    }

    public async Task AddAsync(Invoice entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Invoices.AddAsync(entity, cancellationToken);
    }

    public void Update(Invoice entity)
    {
        _dbContext.Invoices.Update(entity);
    }

    public void Delete(Invoice entity)
    {
        _dbContext.Invoices.Remove(entity);
    }

    public async Task<List<Invoice>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices.Where(i => i.PatientId == patientId).ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetUnpaidAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices
            .Where(i => i.Status == BillingStatus.Pending || i.Status == BillingStatus.PartiallyPaid)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetByStatusAsync(BillingStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invoices.Where(i => i.Status == status).ToListAsync(cancellationToken);
    }
}