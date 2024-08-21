using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.DTOs;

public class DeckDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CardDTO? CommanderCard { get; set; }
    public ICollection<CardDTO>? Cards { get; set; }

    public DeckDTO()
    {
        
    }

    public DeckDTO(Deck deck, CardDTO commanderCard, ICollection<CardDTO> cards)
    {
        Id = deck.Id;
        Name = deck.Name;
        CommanderCard = commanderCard;
        Cards = cards;
    }

    public DeckDTO(Deck deck)
    {
        Id = deck.Id;
        Name = deck.Name;
    }
}
