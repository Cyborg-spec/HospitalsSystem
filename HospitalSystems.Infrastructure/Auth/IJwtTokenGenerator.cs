using HospitalSystems.Domain.Users;

namespace HospitalSystems.Infrastructure.Auth;

public interface IJwtTokenGenerator
{
    Task<TokenResponse> GenerateToken(User user);
    string GenerateRefreshToken();
}