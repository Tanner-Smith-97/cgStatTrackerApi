using Microsoft.AspNetCore.Mvc;
using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Game;
using StatTracker.Interfaces;
using StatTracker.Models;
using StatTracker.Services;

namespace StatTracker.EndPoints;

public class GameEndpoints : IEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/CreateGame", CreateGame);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddTransient<GameService>();
        // Move this to Deck Endpoints at somepoint....
        services.AddTransient<DeckService>();
    }

    public IResult CreateGame(
        GameService gameService,
        PlayerService playerService,
        DeckService deckService,
        MmrService mmrService,
        MyDbContext dbContext,
        [FromBody] List<CreateGameRequest> gameRequestList)
    {
        var participants = new List<GameParticipantDto>();
        
        foreach (var gameRequest in gameRequestList)
        {
            var player = playerService.GetPlayer(gameRequest.PlayerId);
            var deck = deckService.GetDeck(gameRequest.DeckId);

            participants.Add(new GameParticipantDto()
            {
                Player = player,
                Deck = deck,
                Placement = gameRequest.Placement
            });
            
            var result = gameService.CreateGame(gameRequest, player.Mmr, deck.Mmr);
            gameService.AddDeckGamePlayed(deck.Id, gameRequest.Placement);
            gameService.AddPlayerGamePlayed(player.Id, gameRequest.Placement);
        }
        
        mmrService.CalculateMmrChanges(participants);
        
        dbContext.SaveChanges();

        return Results.Ok("sure");
    }
}