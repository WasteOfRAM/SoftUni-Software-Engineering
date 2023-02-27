using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data;

public class SalesContext : DbContext
{
	public SalesContext()
	{
	}

	public SalesContext(DbContextOptions options)
		:base(options)
	{
	}

	public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Sale> Sales { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<Store> Stores { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionStrings = new ConfigurationBuilder()
				.AddUserSecrets<SalesContext>()
				.Build();

			optionsBuilder.UseSqlServer(connectionStrings["ConnectionStrings:Sales"]);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

	}
}
