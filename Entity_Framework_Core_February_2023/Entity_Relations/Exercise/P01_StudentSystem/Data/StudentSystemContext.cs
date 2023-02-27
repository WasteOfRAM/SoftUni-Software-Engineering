using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data;

public class StudentSystemContext : DbContext
{
	public StudentSystemContext()
	{
	}

	public StudentSystemContext(DbContextOptions options) 
		: base(options)
	{
	}

	public virtual DbSet<Student> Students { get; set; } = null!;
	public virtual DbSet<Course> Courses { get; set; } = null!;
	public virtual DbSet<Resource> Resources { get; set; } = null!;
	public virtual DbSet<Homework> Homeworks { get; set; } = null!;
	public virtual DbSet<StudentCourse> StudentsCourses { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionStrings = new ConfigurationBuilder()
				.AddUserSecrets<StartUp>()
				.Build();


			optionsBuilder.UseSqlServer(connectionStrings["ConnectionStrings:StudentSystem"]);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasKey(pk => pk.StudentId);

			entity.Property(p => p.Name).HasMaxLength(100).IsUnicode(true);

			entity.Property(p => p.PhoneNumber).HasMaxLength(10).IsUnicode(false);
		});

		modelBuilder.Entity<Course>(entity =>
		{
            entity.HasKey(pk => pk.CourseId);

			entity.Property(p => p.Price).HasColumnType("money");

            entity.Property(p => p.Name).HasMaxLength(80).IsUnicode(true);

			entity.Property(p => p.Description).IsUnicode(true);
        });

		modelBuilder.Entity<Resource>(entity =>
		{
            entity.HasKey(pk => pk.ResourceId);

            entity.Property(p => p.Name).HasMaxLength(50).IsUnicode(true);

			entity.Property(p => p.Url).IsUnicode(false);

			entity.HasOne(r => r.Course)
				.WithMany(c => c.Resources)
				.HasForeignKey(r => r.CourseId)
				.OnDelete(DeleteBehavior.Restrict);
        });

		modelBuilder.Entity<Homework>(entity =>
		{
			entity.HasKey(pk => pk.HomeworkId);

			entity.Property(p => p.Content).IsUnicode(false);

			entity.HasOne(h => h.Course)
				.WithMany(c => c.Homeworks)
				.HasForeignKey(h => h.CourseId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(h => h.Student)
				.WithMany(s => s.Homeworks)
				.HasForeignKey(h => h.StudentId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<StudentCourse>(entity =>
		{
			entity.HasKey(pk => new { pk.StudentId, pk.CourseId });

			entity
				.HasOne(sc => sc.Student)
				.WithMany(s => s.StudentsCourses)
				.HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
				.HasOne(sc => sc.Course)
				.WithMany(c => c.StudentsCourses)
				.HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        });
	}
}
