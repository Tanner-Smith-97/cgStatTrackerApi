using Miller.MatchMaking.EloRating;
using StatTracker.DbContexts;
using StatTracker.Interfaces;
using StatTracker.Models;

namespace StatTracker.EndPoints;

public class MiscEndpoints : IEndpoint
{
    public void DefineServices(IServiceCollection services)
    {
        
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");

        app.MapGet("/TestData", DoStuff);

        app.MapGet("/GetDeck", GetDeck);
        app.MapPost("/MakeDeck", MakeDeck);

        app.MapGet("/EloStuff/{winner:int}/{loser:int}", EloStuff);
    }
    
    
    IResult MakeDeck(string deckName, StatTrackerDbContext context)
    {
        try
        {
            // query for player id
            // context.Decks.Add(new Deck {DeckName = deckName, PlayerId = });
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.Problem("Someone messed up...");
        }

        return Results.Ok();
    }

    IResult GetDeck(string deckName, StatTrackerDbContext context)
    {
        try
        {
            var stuff = context.Decks.First(x =>
                x.DeckName == deckName
            );

            return Results.Ok(stuff);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    IResult EloStuff(int winner, int loser)
    {
        return Results.Ok((int)EloRating.CalculateEloExchanged(new EloRating(winner), new EloRating(loser)));
    }

    IResult DoStuff()
    {
        var data = new List<TestData>();
        data.Add(new TestData("Anje Falkenrath", false, "1000"));
        data.Add(new TestData("Zurgo Helmsmasher", true, "990"));
        data.Add(new TestData("Prosper, Tome-Bound", false, "880"));

        return Results.Ok(data);
    }
}