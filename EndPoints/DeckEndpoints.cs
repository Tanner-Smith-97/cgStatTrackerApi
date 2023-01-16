using StatTracker.Interfaces;
using StatTracker.Services;

namespace StatTracker.EndPoints;

public class DeckEndpoints : IEndpoint
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<DeckService>();
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("CreateDeck", CreateDeck);
        app.MapGet("GetDeckByName/{deckName:alpha}", GetDeckByName);
        app.MapGet("GetDeckById/{deckId:int}", GetDeckById);
    }

    private IResult CreateDeck(string deckName, string playerName, PlayerService playerService, DeckService deckService)
    {
        var playerId = playerService.GetPlayer(playerName).Id;
        deckService.CreateDeck(deckName, playerId);
        return Results.Ok();
    }

    private IResult GetDeckByName(string deckName, DeckService deckService)
    {
        var deck = deckService.GetDeck(deckName);
        return Results.Ok(deck);
    }

    private IResult GetDeckById(int deckId, DeckService deckService)
    {
        var deck = deckService.GetDeck(deckId);
        return Results.Ok(deck);
    }
}