namespace HospitalSystems.Infrastructure.Auth;

public interface IUserContext
{
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
}