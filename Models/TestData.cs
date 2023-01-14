namespace StatTracker.Models;

public class TestData
{
    public TestData(string deckName, bool winner, string mmr)
    {
        DeckName = deckName;
        Winner = winner;
        Mmr = mmr;
    }

    public string? DeckName { get; set; }
    public bool Winner { get; set; }
    public string? Mmr { get; set; }
}