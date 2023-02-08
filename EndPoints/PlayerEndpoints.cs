using Microsoft.AspNetCore.Mvc;
using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Player;
using StatTracker.Interfaces;
using StatTracker.Services;

namespace StatTracker.EndPoints;

public class PlayerEndpoints : IEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/CreatePlayer", CreatePlayer);
        app.MapGet("/GetPlayer/{playerName}", GetPlayer);
        app.MapGet("players", GetPlayers)
            .Produces<IEnumerable<PlayerEntity>>();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<PlayerService>();
    }

    private IResult GetPlayer(PlayerService player, [FromRoute] string playerName)
    {
        var result = player.GetPlayer(playerName);
        return Results.Ok(result);
    }

    private IResult CreatePlayer(PlayerService player, [FromBody] CreatePlayerRequest request)
    {
        var result = player.CreatePlayer(request);
        return result ? Results.StatusCode(201) : Results.Problem("Something went wrong");
    }

    private IResult GetPlayers(PlayerService playerService)
    {
        var result = playerService.GetPlayers();
        return Results.Ok(result);
    }
}