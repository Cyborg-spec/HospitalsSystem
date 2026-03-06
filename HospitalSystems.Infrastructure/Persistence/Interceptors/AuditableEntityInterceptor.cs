using HospitalSystems.Domain.Common;
using HospitalSystems.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HospitalSystems.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _userContext;

    public AuditableEntityInterceptor(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        // Find all entities inheriting from AuditableEntity that were Added or Modified
        var entries = context.ChangeTracker
            .Entries<AuditableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var utcNow = DateTime.UtcNow;
        var currentUserId = _userContext.UserId; 

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                // Assign Created property
                entry.Property(x => x.CreatedBy).CurrentValue = currentUserId;
            }

            // Always assign Modified property
            entry.Property(x => x.ModifiedBy).CurrentValue = currentUserId;
        }
    }
}
