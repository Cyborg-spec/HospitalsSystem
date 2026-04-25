using HospitalSystems.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    UserManager<User> userManager,RoleManager<IdentityRole<Guid>>roleManager) : IRequestHandler<RegisterCommand, bool>
{
    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("Email is already registered");
        }

        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            request.HospitalId,
            request.DepartmentId)
        {
            TwoFactorEnabled = true,
            EmailConfirmed = true // Crucial bit to ensure TOTP works easily
        };
        var roleExists = await roleManager.RoleExistsAsync(request.Role);
        if (!roleExists)
        {
            throw new Exception($"Role {request.Role} not found");
        }

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Error creating user: {errors}");
        }

        await userManager.AddToRoleAsync(user, request.Role);
        return true;
    }
}
