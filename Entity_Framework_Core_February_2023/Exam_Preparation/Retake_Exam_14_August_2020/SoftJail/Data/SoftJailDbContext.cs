﻿namespace SoftJail.Data
{
    using Microsoft.EntityFrameworkCore;
    using SoftJail.Data.Models;

    public class SoftJailDbContext : DbContext
    {
        public SoftJailDbContext()
        {
        }

        public SoftJailDbContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<Prisoner> Prisoners { get; set; } = null!;

        public DbSet<Officer> Officers { get; set; } = null!;

        public DbSet<Cell> Cells { get; set; } = null!;

        public DbSet<Mail> Mails { get; set; } = null!;

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OfficerPrisoner>(entity =>
            {
                entity.HasKey(pk => new { pk.PrisonerId, pk.OfficerId });

                entity.HasOne(op => op.Prisoner)
                    .WithMany(p => p.PrisonerOfficers)
                    .HasForeignKey(op => op.PrisonerId);

                entity.HasOne(op => op.Officer)
                    .WithMany(o => o.OfficerPrisoners)
                    .HasForeignKey(op => op.OfficerId);
            });
        }
    }
}