using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Integration.Models;
using app.Models;
using app.Repositories;
using app.Repositories.Interfaces;

namespace app.Services;

public class  DeckService
{
    private readonly CardService _cardService;
    private readonly IDeckRepository _deckRepository;
    private readonly IDeckCardRepository _deckCardRepository;
    private readonly UserService _userService;
    private readonly ILanguageRepository _languageRepository; 
    private readonly IRedisRepository _cache;
    public DeckService(
        IDeckRepository deckRepository, 
        UserService userService, 
        CardService cardService, 
        IDeckCardRepository deckCardRepository, 
        ILanguageRepository languageRepository,
        IRedisRepository cache)
    {
        _deckCardRepository = deckCardRepository;
        _deckRepository = deckRepository;
        _userService = userService;
        _cardService = cardService;
        _languageRepository = languageRepository;
        _cache = cache;
    }


    public async Task<ICollection<Deck>> GetDecksAsync(string userEmail)
    {
        ICollection<Deck>? decks = await _cache.GetCacheAsync<ICollection<Deck>>($"decks:user:{userEmail}");
        if(decks is not null)
            return decks;

        User? user = await _userService.GetUserByEmail(userEmail);
        decks = await _deckRepository.GetAllDecksByUserAsync(user);

        _cache.SetCacheAsync($"decks:user:{userEmail}", decks, TimeSpan.FromSeconds(10));
        return decks;        
    }

    public async Task<Deck?> GetDecksByIdAsync(string userEmail, Guid id)
    {
        Deck? deck = await _cache.GetCacheAsync<Deck>($"decks:{id}:user:{userEmail}");
        if(deck is not null)
            return deck;

        User? user = await _userService.GetUserByEmail(userEmail);
        deck = await _deckRepository.GetDeckByUserAndIdUsingIncludesAsync(user, id);

        if(deck is null){
            throw new Exception("Deck not found");
        }

        _cache.SetCacheAsync($"decks:{id}:user:{userEmail}", deck, TimeSpan.FromSeconds(20));
        return deck;
    }

    public async Task<Deck> UpdateDeckNameByIdAsync(string userEmail, Guid id, string deckName)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        Deck? oldDeck = await _deckRepository.GetDeckByUserAndIdAsync(user, id);
        if(oldDeck is null)
        {
            throw new Exception("Deck not found");
        }
        oldDeck.Name = deckName;
        Deck newDeck = await _deckRepository.UpdateDeckAsync(oldDeck);
        return newDeck;
    }

    public async Task<Deck> DeleteDeckByIdAsync(string userEmail, Guid id)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        Deck? deckToBeDeleted = await _deckRepository.GetDeckByUserAndIdAsync(user, id);
        if(deckToBeDeleted is null)
        {
            throw new Exception("Deck not found");
        }
        Deck deletedDeck = await _deckRepository.DeleteDeckAsync(deckToBeDeleted);
        return deletedDeck;
    }

    public async Task<Deck> CreateDeckAsync(string userEmail, CreateDeckDTO deckDto)
    {
        User? user = await _userService.GetUserByEmail(userEmail);

        Card? commanderCard = await _cardService.GetCardByIdAsync(deckDto.CommanderCardId);

        ICollection<Card> cards = await _cardService.GetOneHundredCardsByColorAsync(commanderCard);

        Deck deck = new Deck(user, deckDto.Name, cards);

        Deck createdDeck = await _deckRepository.CreateDeckAsync(deck);

        DeckCard? deckCard = createdDeck.DeckCards.FirstOrDefault(p => p.Card.Id == commanderCard.Id);

        if(deckCard is not null){
            await _deckCardRepository.SetDeckCommanderAsync(deckCard.Id);
        }

        Language? userLanguage = await _languageRepository.GetLanguageByUserAsync(deck.User);

        return deck;
    }

    public async Task<ICollection<Deck>> GetAllDecksAsync()
    {
        ICollection<Deck>? decks =  await _cache.GetCacheAsync<ICollection<Deck>>("decks");
        if(decks is not null)
            return decks;

        decks = await _deckRepository.GetAllDecksAsync();
        _cache.SetCacheAsync("decks", decks, TimeSpan.FromMinutes(1));

        return decks;
    }
}
