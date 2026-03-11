using System.Text.Json;
using HospitalSystems.Domain.AuditLogs;
using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HospitalSystems.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor(IUserContext userContext) : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntitiesAndGenerateAuditLogs(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntitiesAndGenerateAuditLogs(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntitiesAndGenerateAuditLogs(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.State != EntityState.Unchanged && e.State != EntityState.Detached)
            .ToList();

        var utcNow = DateTime.UtcNow;
        var currentUserId = userContext.UserId; 
        
        var auditLogs = new List<AuditLog>();

        foreach (var entry in entries)
        {
            // Do not audit the AuditLog itself! That would cause an infinite loop
            if (entry.Entity is AuditLog) continue;

            // 1. Update the Timestamps & Users inside the Entity itself (if Added or Modified)
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedAt).CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedAt).CurrentValue = utcNow;
                }

                if (entry.Entity is AuditableEntity auditableEvent)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedBy").CurrentValue = currentUserId;
                    }
                    
                    entry.Property("ModifiedBy").CurrentValue = currentUserId;
                }
            }

            // 2. Generate a separate row in the DbContext for our Database AuditLog table
            var entityName = entry.Entity.GetType().Name;
            var action = entry.State.ToString();
            var entityId = entry.Property(x => x.Id).CurrentValue;
            
            var oldValues = new Dictionary<string, object?>();
            var newValues = new Dictionary<string, object?>();

            foreach (var prop in entry.Properties)
            {
                if (prop.IsTemporary || prop.Metadata.IsPrimaryKey())
                    continue;

                string propName = prop.Metadata.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        newValues[propName] = prop.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        oldValues[propName] = prop.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (prop.IsModified)
                        {
                            oldValues[propName] = prop.OriginalValue;
                            newValues[propName] = prop.CurrentValue;
                        }
                        break;
                }
            }

            var oldValuesJson = oldValues.Count == 0 ? null : JsonSerializer.Serialize(oldValues);
            var newValuesJson = newValues.Count == 0 ? null : JsonSerializer.Serialize(newValues);

            var auditLog = new AuditLog(
                currentUserId, 
                action, 
                entityName, 
                entityId, 
                oldValuesJson, 
                newValuesJson
            );
            
            auditLogs.Add(auditLog);
        }

        if (auditLogs.Any())
        {
            context.Set<AuditLog>().AddRange(auditLogs);
        }
    }
}
