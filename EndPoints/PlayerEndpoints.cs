using Microsoft.AspNetCore.Mvc;
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
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddTransient<PlayerService>();
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
}