namespace HospitalSystems.Application.Auth.Commands.Common;

public record LoginResult(bool RequiresTwoFactor,
    Guid? UserId,
    string? AccessToken,
    string? RefreshToken){}
