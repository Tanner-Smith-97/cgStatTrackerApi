namespace StatTracker.Interfaces;

public interface IEndpoint
{
    public void DefineEndpoints(WebApplication app);

    public void DefineServices(WebApplicationBuilder builder);
}