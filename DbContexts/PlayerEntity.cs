using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTracker.DbContexts;

[Table("Players")]
public class PlayerEntity
{
    [Column("Id")] [Key] public int Id { get; set; }

    [Column("Name")] public string Name { get; set; } = default!;

    [Column("Mmr")] public int Mmr { get; set; }

    [Column("GamesPlayed")] public int GamesPlayed { get; set; }

    [Column("GamesWon")] public int GamesWon { get; set; }

    public IEnumerable<DeckEntity> Decks { get; set; } = default!;
    public IEnumerable<GameEntity> Games { get; set; } = default!;
}