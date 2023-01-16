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

    public bool CreateGame(CreateGameRequest request, int playerMmr, int deckMmr, Guid gameId)
    {
        try
        {
            context.Games.Add(new GameDetailEntity
            {
                GameId = gameId,
                PlayerId = request.PlayerId,
                DeckId = request.DeckId,
                Date = request.DatePlayed,
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

    public IEnumerable<GameDetailEntity> GetPreviousGames(int numberOfGames)
    {
        var gameList = context.Games.OrderByDescending(x => x.Date)
            .GroupBy(x => x.GameId)
            .Take(numberOfGames)
            .SelectMany(x => x);
        return gameList;
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