using HospitalSystems.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystems.Infrastructure.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.NationalId)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(p => p.NationalId)
            .IsUnique();

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(200);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Allergies)
            .HasMaxLength(1000);

        builder.Property(p => p.EmergencyContactName)
            .HasMaxLength(200);

        builder.Property(p => p.EmergencyContactPhone)
            .HasMaxLength(20);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
