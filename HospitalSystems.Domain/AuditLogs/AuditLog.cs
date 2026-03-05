using HospitalSystems.Domain.Common;

namespace HospitalSystems.Domain.AuditLogs;

public class AuditLog : BaseEntity
{
    public Guid? UserId { get; private set; }
    public string Action { get; private set; }
    public string EntityType { get; private set; }
    public Guid EntityId { get; private set; }
    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }

    private AuditLog() { }

    public AuditLog(Guid? userId, string action, string entityType, Guid entityId, string? oldValues, string? newValues)
    {
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        OldValues = oldValues;
        NewValues = newValues;
    }
}
