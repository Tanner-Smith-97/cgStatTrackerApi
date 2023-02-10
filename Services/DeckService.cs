using StatTracker.DbContexts;

namespace StatTracker.Services;

public class DeckService
{
    private const int DefaultMmr = 1000;
    private readonly StatTrackerDbContext context;

    public DeckService(StatTrackerDbContext context)
    {
        this.context = context;
    }

    public DeckEntity GetDeck(int deckId)
    {
        try
        {
            var result = context.Decks.First(x => x.Id == deckId);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public DeckEntity GetDeck(string name)
    {
        var result = context.Decks.First(x => x.DeckName == name);
        return result;
    }

    public void CreateDeck(string name, int playerId)
    {
        context.Decks.Add(new DeckEntity { DeckName = name, PlayerId = playerId, Mmr = DefaultMmr });
        context.SaveChanges();
    }

    public IEnumerable<DeckEntity> GetDecks()
    {
        return context.Decks;
    }
}