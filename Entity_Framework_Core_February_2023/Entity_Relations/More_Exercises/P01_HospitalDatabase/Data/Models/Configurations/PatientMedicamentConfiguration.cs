using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.Models.Configurations;

public class PatientMedicamentConfiguration : IEntityTypeConfiguration<PatientMedicament>
{
    public void Configure(EntityTypeBuilder<PatientMedicament> builder)
    {
        builder.ToTable("PatientsMedicaments")
            .HasKey(pk => new { pk.PatientId, pk.MedicamentId });

        builder.HasOne(pm => pm.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(pm => pm.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pm => pm.Medicament)
            .WithMany(m => m.Prescriptions)
            .HasForeignKey(pm => pm.MedicamentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
