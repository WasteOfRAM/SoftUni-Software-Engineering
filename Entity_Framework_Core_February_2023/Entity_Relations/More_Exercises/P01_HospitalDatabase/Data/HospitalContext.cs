using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data.Models.Configurations;

namespace P01_HospitalDatabase.Data;

public class HospitalContext : DbContext
{
	public HospitalContext()
	{
	}

	public HospitalContext(DbContextOptions options)
		: base(options)
	{
	}

	public virtual DbSet<Patient> Patients { get; set; } = null!;
	public virtual DbSet<Visitation> Visitations { get; set; } = null!;
    public virtual DbSet<Diagnose> Diagnoses { get; set; } = null!;
    public virtual DbSet<Medicament> Medicaments { get; set; } = null!;
	public virtual DbSet<PatientMedicament> PatientsMedicaments { get; set; } = null!;
	public virtual DbSet<Doctor> Doctors { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionStrings = new ConfigurationBuilder()
				.AddUserSecrets<HospitalContext>()
				.Build();

			optionsBuilder.UseSqlServer(connectionStrings["ConnectionStrings:Hospital"]);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PatientConfiguration());

		modelBuilder.ApplyConfiguration(new VisitationConfiguration());

		modelBuilder.ApplyConfiguration(new DiagnoseConfiguration());

		modelBuilder.ApplyConfiguration(new MedicamentConfiguration());

		modelBuilder.ApplyConfiguration(new PatientMedicamentConfiguration());

		modelBuilder.ApplyConfiguration(new DoctorConfiguration());
	}
}
