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
        app.MapGet("/GetPreviousGames/{numberOfGames:int}", GetNumberOfGames);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<GameService>();
        services.AddScoped<MmrService>();
    }

    public IResult GetNumberOfGames(int numberOfGames, GameService gameService)
    {
        var gameList = gameService.GetPreviousGames(numberOfGames);
        return Results.Ok(gameList);
    }

    public IResult CreateGame(
        GameService gameService,
        PlayerService playerService,
        DeckService deckService,
        MmrService mmrService,
        StatTrackerDbContext dbContext,
        [FromBody] List<CreateGameRequest> gameRequestList)
    {
        var participants = new List<GameParticipantDto>();

        var gameId = Guid.NewGuid();

        foreach (var gameRequest in gameRequestList)
        {
            var player = playerService.GetPlayer(gameRequest.PlayerId);
            var deck = deckService.GetDeck(gameRequest.DeckId);

            participants.Add(new GameParticipantDto
            {
                Player = player,
                Deck = deck,
                Placement = gameRequest.Placement
            });

            var result = gameService.CreateGame(gameRequest, player.Mmr, deck.Mmr, gameId);
            gameService.AddDeckGamePlayed(deck.Id, gameRequest.Placement);
            gameService.AddPlayerGamePlayed(player.Id, gameRequest.Placement);
        }

        mmrService.CalculateMmrChanges(participants);

        dbContext.SaveChanges();

        return Results.Ok("sure");
    }
}