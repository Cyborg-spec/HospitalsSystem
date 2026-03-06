using HospitalSystems.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your entire Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// THESE TWO ARE CRITICAL: They must be in this exact order, before MapControllers!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();