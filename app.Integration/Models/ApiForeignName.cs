using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace app.Integration.Models;

public class ApiForeignName
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("flavor")]
    public string? Flavor { get; set; }

    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("identifiers")]
    public ApiIdentifiers? Identifiers { get; set; }

    [JsonPropertyName("multiverseid")]
    public int? MultiverseId { get; set; }
}
