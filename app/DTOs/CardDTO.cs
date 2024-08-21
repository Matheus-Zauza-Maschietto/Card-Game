using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.DTOs;

public class CardDTO
{
    public string Name { get; set; }
    public string ManaCost { get; set; }
    public double Cmc { get; set; }
    public ICollection<string> Colors { get; set; }
    public string Type { get; set; }
    public ICollection<string> Types { get; set; }
    public ICollection<string> Subtypes { get; set; }
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
    public ICollection<string> Printings { get; set; }
    public string OriginalText { get; set; }
    public string OriginalType { get; set; }
    public ICollection<LegalityDTO> Legalities { get; set; }

    public CardDTO()
    {
        
    }

    public CardDTO(Card? card)
    {
        Name = card?.Name;
        ManaCost = card?.ManaCost;
        Cmc = card?.Cmc ?? 0;
        Colors = card?.Colors?.Select(c => c?.ColorAbbreviation)?.ToList();
        Type = card?.Type;
        Types = card?.Types?.Select(t => t?.TypeName)?.ToList();
        Subtypes = card?.Subtypes?.Select(s => s?.SubTypeName)?.ToList();
        Rarity = card?.Rarity;
        Set = card?.Set;
        SetName = card?.SetName;
        Text = card?.Text;
        Artist = card?.Artist;
        Number = card?.Number;
        Power = card?.Power;
        Toughness = card?.Toughness;
        Layout = card?.Layout;
        Multiverseid = card?.Multiverseid;
        ImageUrl = card?.ImageUrl;
        Printings = card?.Printings?.Select(p => p?.Name)?.ToList();
        OriginalText = card?.OriginalText;
        OriginalType = card?.OriginalType;
        Legalities = card?.Legalities?.Select(l => new LegalityDTO(l))?.ToList();
    }

    public CardDTO(Card card, ForeignName foreign) 
    {
        Name = foreign.Name;
        ManaCost = card.ManaCost;
        Cmc = card.Cmc;
        Colors = card.Colors.Select(c => c.ColorAbbreviation).ToList();
        Type = foreign.Type;
        Types = card.Types.Select(t => t.TypeName).ToList();
        Subtypes = card.Subtypes.Select(s => s.SubTypeName).ToList();
        Rarity = card.Rarity;
        Set = card.Set;
        SetName = card.SetName;
        Text = foreign.Text;
        Artist = card.Artist;
        Number = card.Number;
        Power = card.Power;
        Toughness = card.Toughness;
        Layout = card.Layout;
        Multiverseid = card.Multiverseid;
        ImageUrl = foreign.ImageUrl;
        Printings = card.Printings.Select(p => p.Name).ToList();
        OriginalText = card.OriginalText;
        OriginalType = card.OriginalType;
        Legalities = card.Legalities.Select(l => new LegalityDTO(l)).ToList();
    }

}
