using HospitalSystems.Application.Auth.Commands.Common;
using HospitalSystems.Domain.Users;
using HospitalSystems.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    UserManager<User> userManager,
    IEmailService emailService) : IRequestHandler<LoginCommand, LoginResult>
{

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Invalid email or password");
        }

        var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

        await emailService.SendAsync(
            user.Email!,
            "Your Hospital Systems Login Code",
            $"Your verification code is: {token}\n\nThis code expires in 10 minutes."
        );

        return new LoginResult(
            RequiresTwoFactor: true,
            UserId: user.Id,
            AccessToken: null,
            RefreshToken: null);
    }
}
