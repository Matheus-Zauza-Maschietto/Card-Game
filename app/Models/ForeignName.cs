using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public int Multiverseid { get; set; }
    public string ScryfallId { get; set; }
    public int MultiverseId { get; set; }
}