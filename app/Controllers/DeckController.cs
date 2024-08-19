using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using app.DTOs;

namespace app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeckController : ControllerBase
{
    private readonly DeckService _deckService; 
    public DeckController(DeckService deckService)
    {
        _deckService = deckService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetDecksAsync()
    {
        try
        {
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            var decks = await _deckService.GetDecksAsync(userEmailClaim ?? string.Empty);
            return Ok(decks.Select(p => new DeckDTO(p, p.Cards.FirstOrDefault())));
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
            var deck = await _deckService.GetDecksByIdAsync(userEmailClaim ?? string.Empty, id);
            return Ok(new DeckDTO(deck, deck.Cards.FirstOrDefault()));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> CreateDeckAsync(CreateDeckDTO deckDTO)
    {
        try
        {
            string? userEmailClaim = User.FindFirstValue(ClaimTypes.Email);
            Deck CreatedDeck = await _deckService.CreateDeckAsync(userEmailClaim ?? string.Empty, deckDTO);
            return Ok(new DeckDTO(CreatedDeck, CreatedDeck.Cards.FirstOrDefault()));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDeckAsync(Guid id)
    {
        try
        {
            return Ok();
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
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
