using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

public class FootballBettingContext : DbContext
{
	public FootballBettingContext()
	{
	}

	public FootballBettingContext(DbContextOptions options)
		: base(options)
	{
	}

	public virtual DbSet<Team> Teams { get; set; } = null!;
    public virtual DbSet<Color> Colors { get; set; } = null!;
    public virtual DbSet<Country> Countries { get; set; } = null!;
    public virtual DbSet<Game> Games { get; set; } = null!;
    public virtual DbSet<Player> Players { get; set; } = null!;
    public virtual DbSet<PlayerStatistic> PlayersStatistics { get; set; } = null!;
    public virtual DbSet<Position> Positions { get; set; } = null!;
    public virtual DbSet<Town> Towns { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Bet> Bets { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionStrings = new ConfigurationBuilder()
				.AddUserSecrets<FootballBettingContext>()
				.Build();

			optionsBuilder.UseSqlServer(connectionStrings["ConnectionStrings:FootballBetting"]);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Team>(entity =>
		{
			entity.HasOne(t => t.PrimaryKitColor)
				.WithMany(c => c.PrimaryKitTeams)
				.HasForeignKey(t => t.PrimaryKitColorId)
				.OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

		modelBuilder.Entity<PlayerStatistic>(entity =>
		{
			entity.HasKey(pk => new { pk.GameId, pk.PlayerId });
		});

		modelBuilder.Entity<Game>(entity =>
		{
			entity.HasOne(g => g.HomeTeam)
				.WithMany(t => t.HomeGames)
				.HasForeignKey(g => g.HomeTeamId)
				.OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });
	}
}
