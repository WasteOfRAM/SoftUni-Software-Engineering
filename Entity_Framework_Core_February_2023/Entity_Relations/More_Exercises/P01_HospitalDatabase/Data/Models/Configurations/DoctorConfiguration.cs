using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.Models.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(p => p.Specialty)
            .HasMaxLength(100)
            .IsUnicode(true);
    }
}
