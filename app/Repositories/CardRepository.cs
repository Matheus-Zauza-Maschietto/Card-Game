using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Models;
using app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories;

public class CardRepository : ICardRepository
{
    private readonly DbSet<Card> _cardContext;
    public CardRepository(ApplicationDbContext context)
    {
        _cardContext = context.Cards;
    }

    public async Task<Card?> GetCardByIdAsync(Guid id)
    {
        return await _cardContext.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
}
