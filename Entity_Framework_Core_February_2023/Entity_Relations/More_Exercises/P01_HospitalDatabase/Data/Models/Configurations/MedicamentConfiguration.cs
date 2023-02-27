using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.Models.Configurations;

public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicaments");

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsUnicode(true);
    }
}
