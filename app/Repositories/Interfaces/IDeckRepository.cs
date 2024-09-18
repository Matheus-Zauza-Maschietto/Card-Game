using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Repositories.Interfaces;

public interface IDeckRepository
{
    Task<ICollection<Deck>> GetAllDecksAsync();
    Task<ICollection<Deck>> GetAllDecksByUserAsync(User user);
    Task<Deck?> GetDeckByUserAndIdAsync(User user, Guid id);
    Task<Deck?> GetDeckByUserAndIdUsingIncludesAsync(User user, Guid id);
    Task<Deck> CreateDeckAsync(Deck deck);
    Task<Deck> DeleteDeckAsync(Deck deck);
    Task<Deck> UpdateDeckAsync(Deck newDeck);
}

