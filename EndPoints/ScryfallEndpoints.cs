using Microsoft.AspNetCore.Mvc;
using StatTracker.EndPoints.Contracts.Scryfall;
using StatTracker.Interfaces;
using StatTracker.Services;

namespace StatTracker.EndPoints;

public class ScryfallEndpoints : IEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/card/image/{cardName}", GetCardImage);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ScryfallService>();
        services.AddSingleton<ImageUriCachingService>();
    }

    private async Task<IResult> GetCardImage(
        ScryfallService scryfall,
        ImageUriCachingService cachingService,
        [FromRoute] string cardName
    )
    {
        if (cachingService.Get(cardName) is not null)
        {
            // If the image exists for that name then just return that
            var uri = cachingService.Get(cardName);
            return Results.Ok(new CardImageResponse()
            {
                ImageUri = uri!.ToString()
            });
        }
        
        // No image exists in cache add it to the cache and then return
        var cardResponse = await scryfall.GetScryfallCardByName(cardName);
        cachingService.Set(cardName, new Uri(cardResponse.ImageUris.ArtCrop));
        return Results.Ok(new CardImageResponse()
        {
            ImageUri = cardResponse.ImageUris.ArtCrop
        });
    }
}