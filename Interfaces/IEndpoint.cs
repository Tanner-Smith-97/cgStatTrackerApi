namespace StatTracker.Interfaces;

public interface IEndpoint
{
    public void DefineServices(IServiceCollection services);
    public void DefineEndpoints(WebApplication app);
}