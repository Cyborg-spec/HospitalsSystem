using HospitalSystems.Domain.Hospitals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystems.Infrastructure.Persistence.Configurations;

public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
{
    public void Configure(EntityTypeBuilder<Hospital> builder)
    {
        builder.ToTable("Hospitals");
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(h => h.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(h => h.Email)
            .HasMaxLength(200);

        builder.HasMany(h => h.Departments)
            .WithOne(d => d.Hospital)
            .HasForeignKey(d => d.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(h => !h.IsDeleted);
    }
}
