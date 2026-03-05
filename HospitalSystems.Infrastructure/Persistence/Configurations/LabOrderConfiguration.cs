using HospitalSystems.Domain.LabOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystems.Infrastructure.Persistence.Configurations;

public class LabOrderConfiguration : IEntityTypeConfiguration<LabOrder>
{
    public void Configure(EntityTypeBuilder<LabOrder> builder)
    {
        builder.ToTable("LabOrders");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.TestType)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(l => l.Doctor)
            .WithMany()
            .HasForeignKey(l => l.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(l => l.MedicalRecordId);

        builder.HasQueryFilter(l => !l.IsDeleted);
    }
}
