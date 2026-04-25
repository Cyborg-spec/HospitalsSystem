using HospitalSystems.Domain.Users;

namespace HospitalSystems.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    Task<TokenResponse> GenerateToken(User user);
    string GenerateRefreshToken();
}
