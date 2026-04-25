using HospitalSystems.Application.Auth.Commands.Common;
using HospitalSystems.Application.Auth.Commands.Login;
using HospitalSystems.Domain.Users;
using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Auth.Commands.Verify;

public class VerifyTwoFactorCommandHandler(
    UserManager<User> userManager,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<VerifyTwoFactorCommand, LoginResult>
{
    public async Task<LoginResult> Handle(VerifyTwoFactorCommand request, CancellationToken ct)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            throw new Exception("User not found.");

        var isValid = await userManager.VerifyTwoFactorTokenAsync(
            user, "Email", request.Code);

        if (!isValid)
            throw new Exception("Invalid or expired code.");

        var tokenResponse = await jwtTokenGenerator.GenerateToken(user);

        return new LoginResult(false, null, tokenResponse.AccessToken, tokenResponse.RefreshToken);
    }
}
