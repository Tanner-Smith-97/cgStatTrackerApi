using StatTracker.DbContexts;

namespace StatTracker.Services;

public class DeckService
{
    private readonly MyDbContext context;

    public DeckService(MyDbContext context)
    {
        this.context = context;
    }

    public DeckEntity GetDeck(int deckId)
    {
        try
        {
            var result = context.Decks.First(x =>
                x.Id == deckId
            );

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}