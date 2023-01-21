using System.ComponentModel.DataAnnotations.Schema;

namespace StatTracker.DbContexts;

[Table("GamesDetails")]
public class GameDetailEntity
{
    [Column("GameId")] public Guid GameId { get; set; }

    [Column("PlayerId")] public int PlayerId { get; set; }

    [Column("DeckId")] public int DeckId { get; set; }

    [Column("PlayerMmr")] public int PlayerMmr { get; set; }

    [Column("DeckMmr")] public int DeckMmr { get; set; }

    [Column("Placement")] public int Placement { get; set; }

    public GameEntity Game { get; set; }
    public DeckEntity Deck { get; set; }
    public PlayerEntity Player { get; set; }
}