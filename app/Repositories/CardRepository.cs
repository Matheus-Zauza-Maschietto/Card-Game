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
    private readonly ApplicationDbContext _context;
    public CardRepository(ApplicationDbContext context)
    {
        _cardContext = context.Cards;
        _context = context;
    }

    public async Task<Card?> GetCardByIdAsync(Guid id)
    {
        return await _cardContext.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ICollection<Card>> GetCardsByIdList(IEnumerable<Guid> ids)
    {
        return await _cardContext.Where(p => ids.Contains(p.Id)).ToListAsync(); 
    }

    public async Task<Card> CreateCard(Card card)
    {
        var createdCard = await _cardContext.AddAsync(card);
        await _context.SaveChangesAsync();
        return createdCard.Entity;
    }
}
