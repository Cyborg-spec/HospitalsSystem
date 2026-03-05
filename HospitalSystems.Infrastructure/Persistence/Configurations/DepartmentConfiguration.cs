using HospitalSystems.Domain.Hospitals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystems.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        builder.Property(d => d.Floor)
            .HasMaxLength(10);

        builder.Property(d => d.RoomNumber)
            .HasMaxLength(20);

        builder.HasOne(d => d.Hospital)
            .WithMany(h => h.Departments)
            .HasForeignKey(d => d.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}
