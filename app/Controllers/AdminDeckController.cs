using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Enums;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[Authorize(Roles = nameof(Roles.ADMIN))]
[ApiController]
[Route("api/[controller]")]
public class AdminDeckController : ControllerBase
{
    private readonly DeckService _deckService;
    public AdminDeckController(DeckService deckService)
    {
        _deckService = deckService;
    }

    
    [HttpGet()]
    public async Task<IActionResult> GetDecksAsync()
    {
        try
        {
            var decks = await _deckService.GetAllDecksAsync();
            return Ok(
                decks.Select(deck => new DeckDTO(
                    deck
                ))
            );
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

}
