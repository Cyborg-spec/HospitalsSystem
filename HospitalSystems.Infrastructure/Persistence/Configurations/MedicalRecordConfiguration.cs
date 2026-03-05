using HospitalSystems.Domain.MedicalRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystems.Infrastructure.Persistence.Configurations;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("MedicalRecords");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.ChiefComplaint)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Symptoms)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.Diagnosis)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.DiagnosisCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(m => m.Vitals)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.Notes)
            .IsRequired()
            .HasMaxLength(5000);

        builder.HasIndex(m => m.PatientId);
        builder.HasIndex(m => m.AppointmentId).IsUnique();

        builder.HasQueryFilter(m => !m.IsDeleted);
    }
}
