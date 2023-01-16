using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTracker.DbContexts;

[Table("Decks")]
public class DeckEntity
{
    [Column("Id")] [Key] public int Id { get; set; }

    [Column("DeckName")] public string DeckName { get; set; } = default!;

    [Column("PlayerId")] public int PlayerId { get; set; }

    [Column("Mmr")] public int Mmr { get; set; }

    [Column("GamesPlayed")] public int GamesPlayed { get; set; }

    [Column("GamesWon")] public int GamesWon { get; set; }
    [Column("ImageUrl")] public string? ImageUrl { get; set; }
    [Column("DeckUrl")] public string? DeckUrl { get; set; }
    public PlayerEntity Player { get; set; } = default!;
    public IEnumerable<GameDetailEntity> Games { get; set; } = default!;
}