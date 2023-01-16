using StatTracker.DbContexts;

namespace StatTracker.Models;

public class GameParticipantDto
{
    public PlayerEntity Player { get; set; }
    public DeckEntity Deck { get; set; }
    public int Placement { get; set; }
}