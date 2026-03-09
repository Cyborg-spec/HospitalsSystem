using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HospitalSystems.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    UserManager<User> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IOptions<JwtSettings> jwtSettings) : IRequestHandler<RegisterCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("Email is already registered");
        }

        var user = new User(request.FirstName, request.LastName, request.Email, request.Role, request.HospitalId,
            request.DepartmentId);
        var result = await userManager.CreateAsync(user,request.Password);
        if (!result.Succeeded)
        {
            throw new Exception("Error creating user");
        }
        var tokenResponse = jwtTokenGenerator.GenerateToken(user);
        // 5. Update user with their shiny new Refresh Token
        user.RefreshToken = tokenResponse.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(jwtSettings.Value.RefreshTokenExpiryDays);
        await userManager.UpdateAsync(user);
        return tokenResponse;
    }
}