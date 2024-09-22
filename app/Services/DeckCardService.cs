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
    private readonly IRedisRepository _cache;
    public DeckCardService(IRedisRepository cache)
    {
        _cache = cache;
    }

    public async Task<DeckCard?> GetDeckCardCommanderByDeck(Deck deck)
    {
        DeckCard? deckCard = await _cache.GetCacheAsync<DeckCard>($"deck:{deck.Id}:commander");
        if(deckCard is not null)
            return deckCard;

        deckCard = deck?.DeckCards?.FirstOrDefault(p => p.IsCommander == true);
        _cache.SetCacheAsync($"deck:{deck.Id}:commander", deckCard, TimeSpan.FromMinutes(1));
        return deckCard;
    }

}
