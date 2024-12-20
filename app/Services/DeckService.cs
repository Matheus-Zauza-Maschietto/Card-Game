using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Enums;
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
    private readonly KafkaService _kafkaService;
    public DeckService(
        IDeckRepository deckRepository, 
        UserService userService, 
        CardService cardService, 
        IDeckCardRepository deckCardRepository, 
        ILanguageRepository languageRepository,
        KafkaService kafkaService
    )
    {
        _kafkaService = kafkaService;
        _deckCardRepository = deckCardRepository;
        _deckRepository = deckRepository;
        _userService = userService;
        _cardService = cardService;
        _languageRepository = languageRepository;
    }


    public async Task<ICollection<Deck>> GetDecksAsync(string userEmail)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        ICollection<Deck>? decks = await _deckRepository.GetAllDecksByUserAsync(user);
        return decks;        
    }

    public async Task<Deck?> GetDecksByIdAsync(string userEmail, Guid id)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        Deck? deck = await _deckRepository.GetDeckByUserAndIdUsingIncludesAsync(user, id);

        if(deck is null){
            throw new Exception("Deck not found");
        }

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

    public async Task<Deck> ImportDeckAsync(string userEmail, ImportDeckDTO importDeckDTO)
    {
        User? user = await _userService.GetUserByEmail(userEmail);
        
        Task<IEnumerable<string>> userRoles = _userService.GetUserRoles(user);
        
        Card? commanderCard = await _cardService.GetCardByIdAsync(importDeckDTO.CommanderCardId);
        
        Deck deck = new Deck(user, importDeckDTO.Name, commanderCard);
        
        Deck createdDeck = await _deckRepository.CreateDeckAsync(deck);

        IEnumerable<ImportDeckKafkaMessage> importMessages =
            importDeckDTO.CardsDTO.Select(p => new ImportDeckKafkaMessage(p, user, createdDeck));
        
        ImportCardsAsync(importMessages, userRoles.Result.Contains(Roles.ADMIN.ToString()));
        
        return createdDeck;
    }

    private async Task ImportCardsAsync(IEnumerable<ImportDeckKafkaMessage> importMessages, bool isAdmin = false)
    {
        if (isAdmin)
            foreach (var importDeckKafkaMessage in importMessages)
            {
                _kafkaService.SendAdminImportation(importDeckKafkaMessage);   
            }
        else
            foreach (var importDeckKafkaMessage in importMessages)
            {
                _kafkaService.SendImportation(importDeckKafkaMessage);   
            }
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
        
        return deck;
    }

    public async Task<ICollection<Deck>> GetAllDecksAsync()
    {
        ICollection<Deck>? decks = await _deckRepository.GetAllDecksAsync();
        return decks;
    }

    public async Task<Deck> GetDeckByIdAsync(Guid id)
    {
        Deck? deck = await _deckRepository.GetDeckByIdAsync(id);

        if (deck is null)
        {
            throw new Exception("Deck not found");
        }

        return deck;
    }

    public async Task<Deck> ImportCardAsync(Card cardsImported, Guid deckId)
    {
        Deck deck = await GetDeckByIdAsync(deckId);
        deck.ImportCards(cardsImported);
        return await _deckRepository.UpdateDeckAsync(deck);
    }
}
