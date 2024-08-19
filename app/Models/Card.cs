using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Integration.Models;

namespace app.Models;

public class Card
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ManaCost { get; set; }
    public double Cmc { get; set; }
    public ICollection<Deck> Decks { get; set; }
    public ICollection<DeckCard> DeckCard { get; set; }
    public ICollection<Color> Colors { get; set; }
    public ICollection<CardColor> CardColors { get; set; }
    public string Type { get; set; }
    public ICollection<Type> Types { get; set; }
    public ICollection<CardType> CardTypes { get; set; }
    public ICollection<SubType> Subtypes { get; set; }
    public ICollection<CardSubType> CardSubtypes { get; set; }
    public string Rarity { get; set; }
    public string Set { get; set; }
    public string SetName { get; set; }
    public string Text { get; set; }
    public string Artist { get; set; }
    public string Number { get; set; }
    public string Power { get; set; }
    public string Toughness { get; set; }
    public string Layout { get; set; }
    public string Multiverseid { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<ForeignName> ForeignNames { get; set; }
    public ICollection<Printing> Printings { get; set; }
    public ICollection<CardPrinting> CardPrintings { get; set; }
    public string OriginalText { get; set; }
    public string OriginalType { get; set; }
    public ICollection<Legality> Legalities { get; set; }

    public Card()
    {
        
    }

    public Card(ApiMagicCard cardApi)
    {
        Name = cardApi.Name ?? string.Empty;
        ManaCost = cardApi.ManaCost ?? string.Empty;
        Cmc = cardApi.Cmc ?? 0;
        Type = cardApi.Type ?? string.Empty;
        Rarity = cardApi.Rarity ?? string.Empty;
        Set = cardApi.Set ?? string.Empty;
        SetName = cardApi.SetName ?? string.Empty;
        Text = cardApi.Text ?? string.Empty;
        Artist = cardApi.Artist ?? string.Empty;
        Number = cardApi.Number ?? string.Empty;
        Power = cardApi.Power ?? string.Empty;
        Toughness = cardApi.Toughness ?? string.Empty;
        Layout = cardApi.Layout ?? string.Empty;
        Multiverseid = cardApi.MultiverseId ?? string.Empty;
        ImageUrl = cardApi.ImageUrl ?? string.Empty;
        OriginalText = cardApi.OriginalText ?? string.Empty;
        OriginalType = cardApi.OriginalType ?? string.Empty;
        Legalities = cardApi.Legalities?.Select(p => new Legality(p))?.ToList() ?? new List<Legality>();
        Printings = cardApi.Printings?.Select(p => new Printing(p))?.ToList() ?? new List<Printing>();
        Types = cardApi.Types?.Select(p => new Type(p))?.ToList() ?? new List<Type>();
        Subtypes = cardApi.Subtypes?.Select(p => new SubType(p))?.ToList() ?? new List<SubType>();
        Colors = cardApi.Colors?.Select(p => new Color(p))?.ToList() ?? new List<Color>();
        ForeignNames = cardApi.ForeignNames?.Select(p => new ForeignName(p))?.ToList() ?? new List<ForeignName>();
    }
}
