using System.Collections.Immutable;
using System.Linq.Expressions;
using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories;

public abstract class BaseRepository<T>(ApplicationDbContext dbContext) : IRepository<T>
    where T : BaseEntity
{
    protected readonly ApplicationDbContext DbContext = dbContext;
    protected DbSet<T> DbSet => DbContext.Set<T>();

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<IImmutableList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await DbSet.ToListAsync(cancellationToken)).ToImmutableList();
    }

    public virtual async Task<IImmutableList<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(predicate).ToListAsync(cancellationToken)).ToImmutableList();
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        DbSet.Remove(entity);
    }
}
