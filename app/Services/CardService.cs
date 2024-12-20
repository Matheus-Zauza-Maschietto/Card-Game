using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Integration.Models;
using app.Models;
using app.Repositories.Interfaces;

namespace app.Services;

public class  CardService
{
    private readonly ICardApiRepository _cardApiRepository;
    private readonly ICardRepository _cardRepository;
    public CardService(ICardRepository cardRepository, ICardApiRepository cardApiRepository)
    {
        _cardRepository = cardRepository;
        _cardApiRepository = cardApiRepository;
    }
    
    public async Task<Card?> GetCardByIdAsync(Guid id)
    {
        Card? card = await _cardRepository.GetCardByIdAsync(id);
        if (card is not null)
        {
            return card;
        }

        ApiMagicCard? apiCard = await _cardApiRepository.GetMagicCardByIdAsync(id);
        if(apiCard is null && card is null)
        {
            throw new Exception("Card not found");
        }

        Card? createdCard = await CreateCardAsync(new Card(apiCard!));
        return createdCard;
    }

    public IEnumerable<CardDTO> SetCardListLanguage(ICollection<Card> cards, Language language)
    {
        foreach(var card in cards)
            yield return SetCardLanguage(card, language);
    }

    public CardDTO SetCardLanguage(Card card, Language language)
    {
        ForeignName? foreignName = card?.ForeignNames?.FirstOrDefault(p => p.Language == language?.LanguageName);

        if(foreignName is not null)
        {
            return new CardDTO(card, foreignName);
        }
        else
        {
            return new CardDTO(card);
        }
    }

    public async Task<ICollection<Card>> GetOneHundredCardsByColorAsync(Card card)
    {
        char? colorAbbreviation = card.Colors?.FirstOrDefault()?.ColorAbbreviation?.FirstOrDefault();

        if(colorAbbreviation is null)
        {
            throw new Exception("Color not found");
        }

        ICollection<ApiMagicCard>? apiCards = await _cardApiRepository.GetOneHundredCardsByColorAsync(colorAbbreviation ?? char.MinValue);

        if(apiCards is null || apiCards.Count == 0)
        {
            throw new Exception("Cards not found");
        }

        List<Card> cards = await GetCardsAsync(apiCards);
        cards.Add(card);

        return cards;
    }

    public async Task<ICollection<Card>> GetCardsListByIdAsync(IEnumerable<Guid> cardsIds)
    {
        IEnumerable<Card> foundedCardsIds = await _cardRepository.GetCardsByIdList(cardsIds);

        IEnumerable<Guid> notFoundedCards = cardsIds.Where(p => !foundedCardsIds.Select(p => p.Id).Contains(p));

        var cardsTasks = notFoundedCards.Select(p => GetCardOnApiById(p));
        Task.WaitAll(cardsTasks);

        var creationCardTask = cardsTasks.Where(p => p.Result is not null).Select(p => _cardRepository.CreateCard(p.Result!));
        Task.WaitAll(creationCardTask);

        foreach (var cardTask in creationCardTask)
        {
            foundedCardsIds.Append(cardTask.Result);
        }
        
        return foundedCardsIds.ToArray();
    }

    private async Task<Card?> GetCardOnApiById(Guid cardId)
    {
        ApiMagicCard? apiCard = await _cardApiRepository.GetMagicCardByIdAsync(cardId);
        if (apiCard is null) return null;
        return new Card(apiCard);
    }

    public async Task<Card> CreateCardAsync(Card card){
        return await _cardRepository.CreateCard(card);
    }

    private async Task<List<Card>> GetCardsAsync(ICollection<ApiMagicCard> apiCards)
    {
        List<Card> cards = new ();
        
        for(int i = 0; i < 99 && i < apiCards.Count - 1; i++)
        {
            cards.Add(new Card(apiCards.ElementAt(i)));
        }

        return cards;
    } 
}
