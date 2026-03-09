namespace HospitalSystems.Domain.Common.Interfaces;

public interface IUserContext
{
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
}