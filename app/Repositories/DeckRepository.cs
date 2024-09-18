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

    public async Task<ICollection<Deck>> GetAllDecksAsync()
    {
        return _deckContext.AsNoTracking().ToList();
    }

    public async Task<ICollection<Deck>> GetAllDecksByUserAsync(User user)
    {
        return await _deckContext.AsNoTracking().Where(p => p.User == user).ToListAsync();
    }

    public async Task<Deck?> GetDeckByUserAndIdAsync(User user, Guid id)
    {
        return await _deckContext
        .FirstOrDefaultAsync(p => p.UserId == user.Id && p.Id == id);
    }

    public async Task<Deck?> GetDeckByUserAndIdUsingIncludesAsync(User user, Guid id)
    {
            return await _deckContext.AsNoTracking()
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.Colors)
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.Printings)
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.Legalities)
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.Types)
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.Subtypes)
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.CardPrintings)
                .Include(e => e.DeckCards)
                    .ThenInclude(p => p.Card)
                        .ThenInclude(e => e.ForeignNames)
                .Include(e => e.User)
                    .ThenInclude(e => e.Language)
                .AsSplitQuery()  
                .FirstOrDefaultAsync(p => p.UserId == user.Id && p.Id == id);
    }

    public async Task<Deck> CreateDeckAsync(Deck deck)
    {
        var createdDeck = await _deckContext.AddAsync(deck);
        _context.SaveChanges();
        return createdDeck.Entity;
    }

    public async Task<Deck> DeleteDeckAsync(Deck deck)
    {
        _deckContext.Remove(deck);
        await _context.SaveChangesAsync();
        return deck;
    }

    public async Task<Deck> UpdateDeckAsync(Deck newDeck)
    {
        var deckUpdated = _deckContext.Update(newDeck);
        await _context.SaveChangesAsync();
        return deckUpdated.Entity;
    }

}
