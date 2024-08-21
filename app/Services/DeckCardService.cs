using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Integration.Models;
using app.Models;
using app.Repositories.Interfaces;

namespace app.Services;

public class DeckCardService
{

    public DeckCardService()
    {

    }

    public DeckCard? GetDeckCardCommanderByDeck(Deck deck)
    {
        return deck?.DeckCards?.FirstOrDefault(p => p.IsCommander == true);
    }
}
