using System.Security.Claims;
using HospitalSystems.Domain.Constants;
using HospitalSystems.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedRolesAndPermissionsAsync(
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<User> userManager)
    {
        // 1. Define the Admin Role
        var adminRoleName = "SuperAdmin";
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            var adminRole = new IdentityRole<Guid>(adminRoleName);
            await roleManager.CreateAsync(adminRole);

            // Give the SuperAdmin all permissions!
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permissions.Users.Manage));
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permissions.Patients.Create));
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permissions.Patients.View));
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permissions.Patients.Edit));
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permissions.Patients.Delete));
        }

        // 2. Define the Doctor Role
        var doctorRoleName = "Doctor";
        if (!await roleManager.RoleExistsAsync(doctorRoleName))
        {
            var doctorRole = new IdentityRole<Guid>(doctorRoleName);
            await roleManager.CreateAsync(doctorRole);

            // Doctors can view and create, but cannot delete
            await roleManager.AddClaimAsync(doctorRole, new Claim("Permission", Permissions.Patients.View));
            await roleManager.AddClaimAsync(doctorRole, new Claim("Permission", Permissions.Patients.Create));
        }

        // 3. Create a Default Admin User (If you don't already have one)
        var adminEmail = "admin@hospital.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User("System", "Admin", adminEmail, null, null);
            await userManager.CreateAsync(adminUser, "Admin123!"); // Use a strong password here
        }

        // Assign the SuperAdmin role to the user if they don't have it
        if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
        {
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}
