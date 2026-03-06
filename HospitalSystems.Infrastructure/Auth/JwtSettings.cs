namespace HospitalSystems.Infrastructure.Auth;

public class JwtSettings
{
    public const string SectionName = "JwtSettings"; // This must match the JSON key
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
}