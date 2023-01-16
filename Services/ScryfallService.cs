using System.Text.Json.Serialization;
using Flurl;
using Flurl.Http;

namespace StatTracker.Services;

public class ScryfallService
{
    public const string SCRYFALL_ROOT_URI = "https://api.scryfall.com";

    public async Task<ScryfallCardResponse> GetScryfallCardByName(string cardName)
    {
        var response = await SCRYFALL_ROOT_URI
            .AppendPathSegment("cards")
            .AppendPathSegment("named")
            .SetQueryParam("fuzzy", cardName)
            .GetJsonAsync<ScryfallCardResponse>();

        return response;
    }
}

public class ScryfallCardResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; }

    [JsonPropertyName("image_uris")] public ScryfallImageResponse ImageUris { get; set; }
}

public class ScryfallImageResponse
{
    [JsonPropertyName("small")] public string Small { get; set; }

    [JsonPropertyName("normal")] public string Normal { get; set; }

    [JsonPropertyName("large")] public string Large { get; set; }

    [JsonPropertyName("png")] public string Png { get; set; }

    [JsonPropertyName("art_crop")] public string ArtCrop { get; set; }

    [JsonPropertyName("border_crop")] public string BoarderCrop { get; set; }
}