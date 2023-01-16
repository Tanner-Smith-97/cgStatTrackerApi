using Miller.MatchMaking.EloRating;
using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Game;
using StatTracker.Models;

namespace StatTracker.Services;

public class MmrService
{
    private readonly StatTrackerDbContext context;
    private readonly PlayerService playerService;
    private readonly DeckService deckService;

    public MmrService(StatTrackerDbContext context, PlayerService playerService, DeckService deckService)
    {
        this.context = context;
        this.playerService = playerService;
        this.deckService = deckService;
    }


    public void CalculateMmrChanges(List<GameParticipantDto> participants)
    {
        var winner = participants.Single(x => x.Placement == 1);
        var loserGroup = participants.Where(x => x.Placement != 1);
        var winnerPlayerMmrDelta = 0;
        var winnerDeckMmrDelta = 0;

        foreach (var deadweight in loserGroup)
        {
            var playerEloDelta = EloRating.CalculateEloExchanged(
                (EloRating)winner.Player.Mmr,
                (EloRating)deadweight.Player.Mmr);
            
            var deckEloDelta = EloRating.CalculateEloExchanged(
                            (EloRating)winner.Player.Mmr,
                            (EloRating)deadweight.Player.Mmr);

            winnerPlayerMmrDelta += playerEloDelta;
            winnerDeckMmrDelta += deckEloDelta;

            deadweight.Player.Mmr -= playerEloDelta;
            deadweight.Deck.Mmr -= playerEloDelta;
        }

        winner.Player.Mmr += winnerPlayerMmrDelta;
        winner.Deck.Mmr += winnerDeckMmrDelta;
    }
}