using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Models;
using app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly DbSet<Deck> _deckContext;
    private readonly ApplicationDbContext _context;
    public DeckRepository(ApplicationDbContext context)
    {
        _context = context;
        _deckContext = context.Decks;
    }

    public async Task<ICollection<Deck>> GetAllDecksByUserAsync(User user)
    {
        return await _deckContext.AsNoTracking().Where(p => p.User == user).ToListAsync();
    }

    public async Task<Deck?> GetDeckByUserAndIdAsync(User user, Guid id)
    {
        return await _deckContext.AsNoTracking().FirstOrDefaultAsync(p => p.User == user && p.Id == id);
    }

    public async Task<Deck> CreateDeckAsync(Deck deck)
    {
        var createdDeck = await _deckContext.AddAsync(deck);
        _context.SaveChanges();
        return deck; // TODO: retornar createdDeck e verificar savechanges
    }

}
