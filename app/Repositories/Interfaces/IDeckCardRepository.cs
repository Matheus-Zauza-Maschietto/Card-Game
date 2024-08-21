using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Repositories.Interfaces;

public interface IDeckCardRepository
{
    Task SetDeckCommanderAsync(int deckCardId);
}
