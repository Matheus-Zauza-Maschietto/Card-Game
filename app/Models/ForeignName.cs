using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Integration.Models;

namespace app.Models;

public class ForeignName
{
    public int Id { get; set; }
    public Card Card { get; set; }
    public Guid CardId { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public string Flavor { get; set; }
    public string ImageUrl { get; set; }
    public string Language { get; set; }
    public string ScryfallId { get; set; }
    public int MultiverseId { get; set; }

    public ForeignName()
    {
        
    }

    public ForeignName(ApiForeignName apiForeignName)
    {
        Name = apiForeignName.Name ?? string.Empty;
        Text = apiForeignName.Text ?? string.Empty;
        Type = apiForeignName.Type ?? string.Empty;
        Flavor = apiForeignName.Flavor ?? string.Empty;
        ImageUrl = apiForeignName.ImageUrl ?? string.Empty;
        Language = apiForeignName.Language ?? string.Empty;
        ScryfallId = apiForeignName.Identifiers?.ScryfallId ?? string.Empty;
        MultiverseId = apiForeignName.MultiverseId ?? 0;
    }

}