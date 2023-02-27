using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.Models.Configurations;

public class VisitationConfiguration : IEntityTypeConfiguration<Visitation>
{
    public void Configure(EntityTypeBuilder<Visitation> builder)
    {
        builder.ToTable("Visitations")
            .HasKey(pk => pk.VisitationId);

        builder.Property(p => p.Comments)
            .HasColumnType("nvarchar(250)");

        builder.HasOne(v => v.Patient)
            .WithMany(p => p.Visitations)
            .HasForeignKey(v => v.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Doctor)
            .WithMany(d => d.Visitations)
            .HasForeignKey(v => v.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
