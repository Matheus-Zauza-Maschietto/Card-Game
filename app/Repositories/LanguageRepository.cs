using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Models;
using app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories;

public class LanguageRepository : ILanguageRepository
{

    private readonly DbSet<Language> _languageContext;
    public LanguageRepository(ApplicationDbContext context)
    {
        _languageContext = context.Languages;
    }

    public async Task<ICollection<Language>> GetAllLanguagesAsync()
    {
        return await _languageContext.AsNoTracking().ToListAsync();
    }

    public async Task<Language?> GetLanguageByUserAsync(User user)
    {
        return await _languageContext.AsNoTracking().FirstOrDefaultAsync(p => p.Users.Contains(user));
    }
}
