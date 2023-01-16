using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTracker.DbContexts;

[Table("Games")]
public class GameEntity
{
    [Key]
    [Column("Id")] public Guid Id { get; set; }
    [Column("Date")] public DateTime Date { get; set; }
    
    public IEnumerable<GameDetailEntity> GameDetail { get; set; }
}