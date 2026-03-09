namespace HospitalSystems.Infrastructure.Auth;

public record TokenResponse(string AccessToken, string RefreshToken,DateTime ExpiresAt);