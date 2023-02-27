using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.Models.Configurations;

public class DiagnoseConfiguration : IEntityTypeConfiguration<Diagnose>
{
    public void Configure(EntityTypeBuilder<Diagnose> builder)
    {
        builder.ToTable("Diagnoses")
            .HasKey(pk => pk.DiagnoseId);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsUnicode(true);

        builder.Property(p => p.Comments)
            .HasMaxLength(250)
            .IsUnicode(true);

        builder.HasOne(d => d.Patient)
            .WithMany(p => p.Diagnoses)
            .HasForeignKey(d => d.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
