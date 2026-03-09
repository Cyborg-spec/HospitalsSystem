using System.Text;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.Appointments;
using HospitalSystems.Domain.Billing;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Domain.LabOrders;
using HospitalSystems.Domain.MedicalRecords;
using HospitalSystems.Domain.Patients;
using HospitalSystems.Domain.Prescriptions;
using HospitalSystems.Domain.Users;
using HospitalSystems.Infrastructure.Auth;
using HospitalSystems.Infrastructure.Persistence;
using HospitalSystems.Infrastructure.Persistence.Interceptors;
using HospitalSystems.Infrastructure.Repositories.Appointments;
using HospitalSystems.Infrastructure.Repositories.Billing;
using HospitalSystems.Infrastructure.Repositories.Hospitals;
using HospitalSystems.Infrastructure.Repositories.LabOrders;
using HospitalSystems.Infrastructure.Repositories.MedicalRecords;
using HospitalSystems.Infrastructure.Repositories.Patients;
using HospitalSystems.Infrastructure.Repositories.Prescriptions;
using HospitalSystems.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HospitalSystems.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Register Interceptor as Singleton to avoid EF Core internal service provider rebuilds
        services.AddSingleton<AuditableEntityInterceptor>();

        // 2. Register ApplicationDbContext and attach the interceptor
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();

            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(interceptor);
        });

        // 3. Register Identity services to work with IdentityDbContext
        services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // 4. Register Repositories and Unit of Work
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IHospitalRepository, HospitalRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<ILabOrderRepository, LabOrderRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        // 5. Auth and JWT configuration
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IUserContext, UserContext>();

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        // 6. Configure Authentication
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequiresAdmin", policy =>
                policy.RequireRole(nameof(UserRole.SuperAdmin), nameof(UserRole.HospitalAdmin)));
            options.AddPolicy("CanCreatePatient",
                policy => policy.RequireRole(nameof(UserRole.Nurse), nameof(UserRole.Receptionist),
                    nameof(UserRole.Doctor)));
        });
        services.AddHttpContextAccessor();

        return services;
    }
}