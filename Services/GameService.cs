using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Game;

namespace StatTracker.Services;

public class GameService
{
    private readonly MyDbContext context;
    private readonly PlayerService playerService;
    private readonly DeckService deckService;

    public GameService(MyDbContext context, PlayerService playerService, DeckService deckService)
    {
        this.context = context;
        this.playerService = playerService;
        this.deckService = deckService;
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public bool CreateGame(CreateGameRequest request, int playerMmr, int deckMmr)
    {
        try
        {
            context.Games.Add(new GameEntity
            {
                PlayerId = request.PlayerId, DeckId = request.DeckId, Date = request.DatePlayed,
                Placement = request.Placement, PlayerMmr = playerMmr, DeckMmr = deckMmr
            });
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public void AddPlayerGamePlayed(int playerId, int placement)
    {
        var player = this.playerService.GetPlayer(playerId);
        player.GamesPlayed += 1;
        if (placement == 1)
        {
            player.GamesWon += 1;
        }
    }

    public void AddDeckGamePlayed(int deckId, int placement)
    {
        var deck = this.deckService.GetDeck(deckId);
        deck.GamesPlayed += 1;
        if (placement == 1)
        {
            deck.GamesWon += 1;
        }
    }
}