using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HospitalSystems.Application.Auth.Commands.Refresh;

public class RefreshCommandHandler(
    UserManager<User> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IOptions<JwtSettings> jwtOptions) : IRequestHandler<RefreshCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken,
            cancellationToken);
        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new Exception("Invalid or expired refresh token");
        }

        var tokenResponse = await jwtTokenGenerator.GenerateToken(user);
        user.RefreshToken =  tokenResponse.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryDays);
        await userManager.UpdateAsync(user);
        return tokenResponse;
    }
}