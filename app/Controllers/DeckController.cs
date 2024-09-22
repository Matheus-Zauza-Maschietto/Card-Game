using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using app.DTOs;
using app.Migrations;
using app.Validators;
using FluentValidation;
using app.Repositories.Interfaces;

namespace app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeckController : ControllerBase
{
    private readonly IRedisRepository _cache;
    private readonly DeckService _deckService;
    private readonly CardService _cardService;
    private readonly DeckCardService _deckCardService;
    public DeckController(
        DeckService deckService, 
        DeckCardService deckCardService, 
        CardService cardService,
        IRedisRepository cache)
    {
        _deckCardService = deckCardService;
        _deckService = deckService;
        _cardService = cardService;
        _cache = cache;
    }

    [HttpGet()]
    public async Task<IActionResult> GetDecksAsync()
    {
        try
        {
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            IEnumerable<DeckDTO>? decksDTO = await _cache.GetCacheAsync<IEnumerable<DeckDTO>>($"decks:user:{userEmailClaim}");
            if(decksDTO is not null)
                return Ok(decksDTO);

            var decks = await _deckService.GetDecksAsync(userEmailClaim ?? string.Empty);
            decksDTO = decks.Select(deck => new DeckDTO(deck));
            _cache.SetCacheAsync($"decks:user:{userEmailClaim}", decksDTO, TimeSpan.FromSeconds(10));
            return Ok(decksDTO);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }


    [HttpPost()]
    public async Task<IActionResult> CreateDeckAsync(CreateDeckDTO deckDTO, CreateDeckDTOValidator validator)
    {
        try
        {
            validator.ValidateAndThrow(deckDTO);
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            Deck createdDeck = await _deckService.CreateDeckAsync(userEmailClaim ?? string.Empty, deckDTO);
            Card? commanderCard =  (await _deckCardService.GetDeckCardCommanderByDeck(createdDeck))?.Card;
            DeckDTO deck = new DeckDTO(
                    createdDeck,
                    _cardService.SetCardLanguage(commanderCard, createdDeck.User.Language),
                    _cardService.SetCardListLanguage(createdDeck.DeckCards.Select(p => p.Card).ToList(), createdDeck.User.Language).ToList()
                );
            return Ok(deck);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeckAsync(Guid id)
    {
        try
        {
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            DeckDTO? deckDTO = await _cache.GetCacheAsync<DeckDTO>($"deck:{id}:user:{userEmailClaim}");
            if(deckDTO is not null)
                return Ok(deckDTO);

            Deck? deck = await _deckService.GetDecksByIdAsync(userEmailClaim ?? string.Empty, id);
            Card? commanderCard = (await _deckCardService.GetDeckCardCommanderByDeck(deck))?.Card;
            deckDTO = new DeckDTO(
                    deck,
                    _cardService.SetCardLanguage(commanderCard, deck.User.Language),
                    _cardService.SetCardListLanguage(deck.DeckCards.Select(p => p.Card).ToList(), deck.User.Language).ToList()
                );

            _cache.SetCacheAsync($"deck:{id}:user:{userEmailClaim}", deckDTO, TimeSpan.FromSeconds(15));
            return Ok(
                deckDTO
            );
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDeckNameAsync(Guid id, string name)
    {
        try
        {
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            Deck deck = await _deckService.UpdateDeckNameByIdAsync(userEmailClaim ?? string.Empty, id, name);
            return Ok(
                    new DeckDTO(
                        deck
                    )
                );
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeckAsync(Guid id)
    {
        try
        {
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            Deck deletedDeck = await _deckService.DeleteDeckByIdAsync(userEmailClaim ?? string.Empty, id);
            return Ok(
            new DeckDTO(
                deletedDeck
            )
        );
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
