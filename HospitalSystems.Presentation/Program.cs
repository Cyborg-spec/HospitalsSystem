using System.Text.Json.Serialization;
using HospitalSystems.Application;
using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // This tells the API to accept "Doctor" instead of 1, both for incoming requests and outgoing responses!
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Register your entire Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Hospital Systems API";
    options.AddSecurity("Bearer", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    options.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Ensure database is created (optional, if you already applied migrations you don't strictly need this, but good for local dev)
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        await DatabaseSeeder.SeedRolesAndPermissionsAsync(roleManager, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

// THESE TWO ARE CRITICAL: They must be in this exact order, before MapControllers!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();