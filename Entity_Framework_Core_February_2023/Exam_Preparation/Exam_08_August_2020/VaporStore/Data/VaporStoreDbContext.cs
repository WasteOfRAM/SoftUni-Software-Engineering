namespace VaporStore.Data
{
    using Microsoft.EntityFrameworkCore;
    using VaporStore.Data.Models;

    public class VaporStoreDbContext : DbContext
    {
        public VaporStoreDbContext()
        {
        }

        public VaporStoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Developer> Developers { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GameTag> GameTags { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Purchase> Purchases { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<GameTag>(entity => 
            {
                entity.HasKey(pk => new { pk.TagId, pk.GameId });

                entity.HasOne(gt => gt.Tag)
                    .WithMany(t => t.GameTags)
                    .HasForeignKey(gt => gt.TagId);

                entity.HasOne(gt => gt.Game)
                    .WithMany(g => g.GameTags)
                    .HasForeignKey(gt => gt.GameId);
            });
        }
    }
}