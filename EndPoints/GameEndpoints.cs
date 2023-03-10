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
        var gameEntities = gameList.ToList();
        var gameDetails = gameService.GetPreviousGameDetails(gameEntities);
        return Results.Ok(gameDetails);
    }

    public IResult CreateGame(
        GameService gameService,
        PlayerService playerService,
        DeckService deckService,
        MmrService mmrService,
        StatTrackerDbContext dbContext,
        [FromBody] CreateGameRequest gameRequest)
    {
        var participants = new List<GameParticipantDto>();

        // var gameId = Guid.NewGuid();
        var gameId = gameService.CreateGame(gameRequest.DatePlayed);
        
        foreach (var gameDetail in gameRequest.GameDetailsList)
        {
            var player = playerService.GetPlayer(gameDetail.PlayerId);
            var deck = deckService.GetDeck(gameDetail.DeckId);

            participants.Add(new GameParticipantDto
            {
                Player = player,
                Deck = deck,
                Placement = gameDetail.Placement
            });

            var result = gameService.AddGameDetails(gameDetail, player.Mmr, deck.Mmr, gameId);
            gameService.AddDeckGamePlayed(deck.Id, gameDetail.Placement);
            gameService.AddPlayerGamePlayed(player.Id, gameDetail.Placement);
        }

        mmrService.CalculateMmrChanges(participants);

        dbContext.SaveChanges();

        return Results.Ok("sure");
    }

    // public IResult DeleteMostRecentGame()
    // {
    //     
    // }
}