using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StatTracker.EndPoints.Contracts.Player;

public class CreatePlayerRequest
{
    [JsonPropertyName("PlayerName")]
    [Required]
    public string name { get; set; }
}