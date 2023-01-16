using System.Text.Json.Serialization;

namespace StatTracker.EndPoints.Contracts.Scryfall;

public class CardImageResponse
{
    [JsonPropertyName("image_uri")] public string ImageUri { get; set; }
}