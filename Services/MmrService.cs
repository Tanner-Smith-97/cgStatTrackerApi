using Miller.MatchMaking.EloRating;
using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Game;

namespace StatTracker.Services;

public class MmrService
{
    private readonly MyDbContext context;
    private readonly PlayerService playerService;
    private readonly DeckService deckService;

    public MmrService(MyDbContext context, PlayerService playerService, DeckService deckService)
    {
        this.context = context;
        this.playerService = playerService;
        this.deckService = deckService;
    }


    public void CalculateMmrChanges(List<CreateGameRequest> request)
    {
        var winner = request.Single(x => x.Placement == 1);
        var loserGroup = request.Where(x => x.Placement != 1);
        var winnerMmrChange = 0;

        foreach (var deadweight in loserGroup)
        {
            // (int)EloRating.CalculateEloExchanged(new EloRating(winner), new EloRating(loser));
            // (int)EloRating.CalculateEloExchanged((EloRating)winner., new EloRating(loser));
        }
    }
    
}