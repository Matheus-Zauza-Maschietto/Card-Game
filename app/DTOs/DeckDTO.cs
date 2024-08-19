using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.DTOs;

public class DeckDTO
{
    public string Name { get; set; }
    public CardDTO? CommanderCard { get; set; }
    public ICollection<CardDTO>? Cards { get; set; }

    public DeckDTO()
    {
        
    }

    public DeckDTO(Deck deck, Card commanderCard)
    {
        Name = deck.Name;
        CommanderCard = new CardDTO(commanderCard);
        Cards = deck.Cards.Select(p => new CardDTO(p)).ToList();
    }
}
