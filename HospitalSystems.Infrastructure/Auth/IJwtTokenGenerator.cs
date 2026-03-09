using HospitalSystems.Domain.Users;

namespace HospitalSystems.Infrastructure.Auth;

public interface IJwtTokenGenerator
{
    TokenResponse GenerateToken(User user);
    string GenerateRefreshToken();
}