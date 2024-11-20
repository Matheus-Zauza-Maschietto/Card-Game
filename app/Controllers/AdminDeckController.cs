using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Enums;
using app.Repositories.Interfaces;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[Authorize(Roles = nameof(Roles.ADMIN))]
[ApiController]
[Route("api/[controller]")]
public class AdminDeckController : ControllerBase
{
    private readonly IRedisRepository _cache;
    private readonly DeckService _deckService;
    public AdminDeckController(DeckService deckService, IRedisRepository cache)
    {
        _deckService = deckService;
        _cache = cache;
    }
    
    [AllowAnonymous]
    [HttpGet("/health-check")]
    public IActionResult HealthCheck() => Ok();
    
    
    [HttpGet()]
    public async Task<IActionResult> GetDecksAsync()
    {
        try
        {
            IEnumerable<DeckDTO>? decksDTO = await _cache.GetCacheAsync<IEnumerable<DeckDTO>>("decks:admin");
            if(decksDTO is not null)
                return Ok(decksDTO);

            var decks = await _deckService.GetAllDecksAsync();
            decksDTO = decks.Select(deck => new DeckDTO(deck));
            _cache.SetCacheAsync("decks:admin", decksDTO, TimeSpan.FromSeconds(10));

            return Ok(decksDTO);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

}
