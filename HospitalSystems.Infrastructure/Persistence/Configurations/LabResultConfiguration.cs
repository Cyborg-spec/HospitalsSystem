using HospitalSystems.Domain.LabOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystems.Infrastructure.Persistence.Configurations;

public class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
{
    public void Configure(EntityTypeBuilder<LabResult> builder)
    {
        builder.ToTable("LabResults");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ResultData)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(r => r.Notes)
            .HasMaxLength(2000);

        builder.Property(r => r.FilePath)
            .HasMaxLength(500);

        builder.HasIndex(r => r.LabOrderId)
            .IsUnique();

        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}
