using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Integration.Models;

namespace app.Repositories.Interfaces;

public interface ICardApiRepository
{

    Task<ICollection<ApiMagicCard>?> GetOneHundredCardsAsync(int page);
    Task<ICollection<ApiMagicCard>?> GetOneHundredCardsByColorAsync(char color);
    Task<ApiMagicCard?> GetMagicCardByIdAsync(Guid id);    
}
