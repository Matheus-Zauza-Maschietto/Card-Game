using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace app.Integration.Models;

public class ApiIdentifiers
{
    [JsonPropertyName("scryfallId")]
    public string? ScryfallId { get; set; }

    [JsonPropertyName("multiverseId")]
    public int? MultiverseId { get; set; }
}

