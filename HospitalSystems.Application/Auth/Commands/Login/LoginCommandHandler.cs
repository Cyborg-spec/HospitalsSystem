using HospitalSystems.Application.Auth.Commands;
using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HospitalSystems.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    UserManager<User> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IOptions<JwtSettings> jwtOptions) : IRequestHandler<LoginCommand,TokenResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Invalid email or password");
        }

        var tokenResponse = await _jwtTokenGenerator.GenerateToken(user);
        user.RefreshToken = tokenResponse.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);

        await _userManager.UpdateAsync(user);
        return tokenResponse;
    }
}