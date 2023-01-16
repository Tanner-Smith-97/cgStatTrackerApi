using StatTracker.Interfaces;

namespace StatTracker.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpointDefinitions = new List<IEndpoint>();

        foreach (var marker in scanMarkers)
        {
            endpointDefinitions.AddRange(
                marker.Assembly.ExportedTypes
                   .Where(x => typeof(IEndpoint).IsAssignableFrom(x) &&
                               !x.IsAbstract &&
                               !x.IsInterface)
                   .Select(Activator.CreateInstance)
                   .Cast<IEndpoint>());
        }

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineServices(services);
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpoint>);

        return services;
    }

    public static void UseEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpoint>>();

        foreach (var endpointDefinition in definitions)
        {
            endpointDefinition.DefineEndpoints(app);
        }
    }
}