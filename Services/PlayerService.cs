using StatTracker.DbContexts;
using StatTracker.EndPoints.Contracts.Player;

namespace StatTracker.Services;

public class PlayerService
{
    private const int DefaultMmr = 1000;
    private readonly MyDbContext context;

    public PlayerService(MyDbContext context)
    {
        this.context = context;
    }

    public bool CreatePlayer(CreatePlayerRequest request)
    {
        try
        {
            context.Players.Add(new PlayerEntity { Name = request.name, Mmr = DefaultMmr });
            context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public PlayerEntity GetPlayer(string playerName)
    {
        try
        {
            var result = context.Players.First(x =>
                x.Name == playerName
            );

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public PlayerEntity GetPlayer(int playerId)
    {
        try
        {
            var result = context.Players.First(x =>
                x.Id == playerId
            );

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}