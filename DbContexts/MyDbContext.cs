using Microsoft.EntityFrameworkCore;

namespace StatTracker.DbContexts;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> context) : base(context)
    {
    }

    public DbSet<DeckEntity> Decks { get; set; } = default!;
    public DbSet<PlayerEntity> Players { get; set; } = default!;
    public DbSet<GameEntity> Games { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameEntity>()
            .HasKey(x => new { x.GameId, x.PlayerId });

        modelBuilder.Entity<GameEntity>()
            .HasOne(x => x.Player)
            .WithMany(x => x.Games);

        modelBuilder.Entity<GameEntity>()
            .HasOne(x => x.Deck)
            .WithMany(x => x.Games);

        modelBuilder.Entity<PlayerEntity>()
            .HasMany(x => x.Decks)
            .WithOne(x => x.Player);
        base.OnModelCreating(modelBuilder);
    }
}