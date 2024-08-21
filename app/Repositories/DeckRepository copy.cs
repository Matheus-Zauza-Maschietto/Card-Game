using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Models;
using app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories;

public class DeckCardRepository : IDeckCardRepository
{
    private readonly DbSet<DeckCard> _deckCardContext;
    private readonly ApplicationDbContext _context;
    public DeckCardRepository(ApplicationDbContext context)
    {
        _context = context;
        _deckCardContext = context.DeckCards;
    }


    public async Task SetDeckCommanderAsync(int deckCardId)
    {
        DeckCard? deckCardFounded = await _deckCardContext.FirstOrDefaultAsync(p => p.Id == deckCardId);
        if(deckCardFounded is null)
        {
           throw new Exception("DeckCard not found");
        }
        deckCardFounded.IsCommander = true;
        await _context.SaveChangesAsync();
    }
}
