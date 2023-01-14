using Microsoft.EntityFrameworkCore;
using Miller.MatchMaking.EloRating;
using StatTracker.DbContexts;
using StatTracker.EndPoints;
using StatTracker.Models;

var builder = WebApplication.CreateBuilder(args);

var playerEndpoint = new PlayerEndpoints();
playerEndpoint.DefineServices(builder);
var gameEndpoint = new GameEndpoints();
gameEndpoint.DefineServices(builder);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        policy => { policy.WithOrigins("http://localhost:4200"); });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
    options.UseMySql("Server=localhost;Database=StatTrackerDB; Uid=root; Pwd=MTNspark33!;", serverVersion);
});

var app = builder.Build();
playerEndpoint.DefineEndpoints(app);
gameEndpoint.DefineEndpoints(app);

app.UseCors(myAllowSpecificOrigins);

app.MapGet("/", () => "Hello World!");

app.MapGet("/TestData", DoStuff);

app.MapGet("/GetDeck", GetDeck);
app.MapPost("/MakeDeck", MakeDeck);

app.MapGet("/EloStuff/{winner}/{losser}", EloStuff);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

IResult MakeDeck(string deckName, MyDbContext context)
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
        throw;
    }

    return Results.Ok();
}

IResult GetDeck(string deckName, MyDbContext context)
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

IResult EloStuff(int winner, int losser)
{
    return Results.Ok((int)EloRating.CalculateEloExchanged(new EloRating(winner), new EloRating(losser)));
}

IResult DoStuff()
{
    var data = new List<TestData>();
    data.Add(new TestData("Anje Falkenrath", false, "1000"));
    data.Add(new TestData("Zurgo Helmsmasher", true, "990"));
    data.Add(new TestData("Prosper, Tome-Bound", false, "880"));

    return Results.Ok(data);
}