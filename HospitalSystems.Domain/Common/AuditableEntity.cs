namespace HospitalSystems.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public Guid? CreatedBy { get; private set; }
    public Guid? ModifiedBy { get; private set; }
}