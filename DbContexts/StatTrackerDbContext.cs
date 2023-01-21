using Microsoft.EntityFrameworkCore;

namespace StatTracker.DbContexts;

public class StatTrackerDbContext : DbContext
{
    public StatTrackerDbContext(DbContextOptions<StatTrackerDbContext> context) : base(context)
    {
    }

    public DbSet<DeckEntity> Decks { get; set; } = default!;
    public DbSet<PlayerEntity> Players { get; set; } = default!;
    public DbSet<GameDetailEntity> GameDetails { get; set; } = default!;
    public DbSet<GameEntity> Games { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameEntity>()
            .HasMany(x => x.GameDetail)
            .WithOne(x => x.Game);

        modelBuilder.Entity<GameDetailEntity>()
            .HasKey(x => new { x.GameId, x.PlayerId });

        modelBuilder.Entity<GameDetailEntity>()
            .HasOne(x => x.Player)
            .WithMany(x => x.Games);

        modelBuilder.Entity<GameDetailEntity>()
            .HasOne(x => x.Deck)
            .WithMany(x => x.Games);

        modelBuilder.Entity<PlayerEntity>()
            .HasMany(x => x.Decks)
            .WithOne(x => x.Player);
        base.OnModelCreating(modelBuilder);
    }

    // private void SeedData(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity { Name = "Tanner", Id = 0 },
    //         new PlayerEntity { Name = "Kayden", Id = 1 },
    //         new PlayerEntity { Name = "Clark", Id = 2 },
    //         new PlayerEntity { Name = "Josh", Id = 3 });
    //     modelBuilder.Entity<DeckEntity>().HasData(new DeckEntity { DeckName = "Anje", PlayerId = 0}, 
    //         new DeckEntity { DeckName = "Slivers", PlayerId = 1 },
    //         new DeckEntity { DeckName = "LandOP", PlayerId = 2 },
    //         new DeckEntity { DeckName = "BigDeckEnergy", PlayerId = 3 });
    // }
}