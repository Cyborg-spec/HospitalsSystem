using HospitalSystems.Domain.Users;

namespace HospitalSystems.Infrastructure.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}