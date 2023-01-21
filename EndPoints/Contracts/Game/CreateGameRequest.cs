using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StatTracker.EndPoints.Contracts.Game;

public class CreateGameRequest
{
    [JsonPropertyName("DatePlayed")]
    public DateTime DatePlayed { get; set; } = DateTime.Now;

    public List<CreateGameDetails> GameDetailsList { get; set; }
}


public class CreateGameDetails
{
    [JsonPropertyName("PlayerId")]
    [Required]
    public int PlayerId { get; set; }

    [JsonPropertyName("DeckId")]
    [Required]
    public int DeckId { get; set; }

    [JsonPropertyName("Placement")]
    [Required]
    public int Placement { get; set; }
}