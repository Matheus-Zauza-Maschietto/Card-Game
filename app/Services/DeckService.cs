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

public class DeckService
{
    private readonly CardService _cardService;
    private readonly IDeckRepository _deckRepository;
    private readonly UserService _userService;
    public DeckService(IDeckRepository deckRepository, UserService userService, CardService cardService)
    {
        _deckRepository = deckRepository;
        _userService = userService;
        _cardService = cardService;
    }


    public async Task<ICollection<Deck>> GetDecksAsync(string userEmail)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        ICollection<Deck> decks = await _deckRepository.GetAllDecksByUserAsync(user);
        return decks;        
    }

    public async Task<Deck?> GetDecksByIdAsync(string userEmail, Guid id)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        Deck? deck = await _deckRepository.GetDeckByUserAndIdAsync(user, id);

        if(deck is null){
            throw new Exception("Deck not found");
        }

        return deck;
    }

    public async Task<Deck> CreateDeckAsync(string userEmail, CreateDeckDTO deckDto)
    {
        User? user = await _userService.GetUserByEmail(userEmail);

        Card? card = await _cardService.GetCardByIdAsync(deckDto.CommanderCardId);

        ICollection<Card> cards = await _cardService.GetOneHundredCardsByColorAsync(card);

        Deck deck = new Deck(user, deckDto.Name, cards);

        Deck createdDeck = await _deckRepository.CreateDeckAsync(deck);

        return deck;
    }
}
