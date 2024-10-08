using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Repositories.Interfaces;
public interface  ILanguageRepository
{
    Task<ICollection<Language>> GetAllLanguagesAsync();
    Task<Language?> GetLanguageByUserAsync(User user);
}
