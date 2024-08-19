using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace app.Integration.Models;

public class ApiLegality
{
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("legality")]
    public string? LegalityStatus { get; set; }
}