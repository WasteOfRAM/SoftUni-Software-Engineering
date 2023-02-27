using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.Models.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients")
            .HasKey(e => e.PatientId);

        builder.Property(p => p.FirstName)
            .HasColumnType("nvarchar(50)");

        builder.Property(p => p.LastName)
            .HasColumnType("nvarchar(50)");

        builder.Property(p => p.Address)
            .HasColumnType("nvarchar(250)");

        builder.Property(p => p.Email)
            .HasColumnType("varchar(80)");
    }
}
