using Microsoft.EntityFrameworkCore;
using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Game;

namespace StatTracker.Services;

public class GameService
{
    private readonly StatTrackerDbContext context;
    private readonly DeckService deckService;
    private readonly PlayerService playerService;

    public GameService(StatTrackerDbContext context, PlayerService playerService, DeckService deckService)
    {
        this.context = context;
        this.playerService = playerService;
        this.deckService = deckService;
    }

    public Guid CreateGame(DateTime date)
    {
        var gameId = Guid.NewGuid();
        context.Games.Add(new GameEntity
        {
            Id = gameId,
            Date = date
        });

        return gameId;
    }

    public bool AddGameDetails(CreateGameDetails request, int playerMmr, int deckMmr, Guid gameId)
    {
        try
        {
            context.GameDetails.Add(new GameDetailEntity
            {
                GameId = gameId,
                PlayerId = request.PlayerId,
                DeckId = request.DeckId,
                Placement = request.Placement,
                PlayerMmr = playerMmr,
                DeckMmr = deckMmr
            });
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public IEnumerable<GameEntity> GetPreviousGames(int numberOfGames)
    {
        var gameList = context.Games.OrderByDescending(x => x.Date).Take(numberOfGames);
        return gameList;
    }

    // public IEnumerable<GameDetailEntity> GetPreviousGameDetails(IEnumerable<GameEntity> gamesList)
    // {
    //     var gameIds = gamesList.Select(x => x.Id);
    //     return context.GameDetails.Where(x =>  gameIds.Contains(x.GameId));
    // }
    
    public IEnumerable<GameEntity> GetPreviousGameDetails(IEnumerable<GameEntity> gamesList)
    {
        var gameIds = gamesList.Select(x => x.Id);

        return context.Games
            .Include(g => g.GameDetail)
            .Where(g => gameIds.Contains(g.Id));
    }

    public void AddPlayerGamePlayed(int playerId, int placement)
    {
        var player = playerService.GetPlayer(playerId);
        player.GamesPlayed += 1;
        if (placement == 1) player.GamesWon += 1;
    }

    public void AddDeckGamePlayed(int deckId, int placement)
    {
        var deck = deckService.GetDeck(deckId);
        deck.GamesPlayed += 1;
        if (placement == 1) deck.GamesWon += 1;
    }
}