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
    public ICollection<Card> Cards { get; set; }
    public ICollection<DeckCard> DeckCards { get; set; }
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

}
   