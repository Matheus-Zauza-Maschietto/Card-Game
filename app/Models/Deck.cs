using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;

namespace app.Models;

public class Deck
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Card> Cards { get; set; } = new List<Card>();
    public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
    public string UserId { get; set; }
    public User User { get; set; }

    public Deck()
    {
        
    }

    public Deck(User user, string name, ICollection<Card> cards)
    {
        User = user;
        UserId = user.Id;
        Name = name;
        Cards = cards;
    }
    
    public Deck(User user, string name, Card commanderCard)
    {
        User = user;
        UserId = user.Id;
        Name = name;
        Cards = new List<Card>(){ commanderCard };
    }

    public Deck ImportCards(Card card)
    {
        Cards.Add(card);
        return this;
    }
}
   