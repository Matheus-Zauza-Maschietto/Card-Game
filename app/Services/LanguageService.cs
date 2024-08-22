using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Repositories.Interfaces;

namespace app.Services;

public class LanguageService
{
    private readonly ILanguageRepository _languageRepository;   
    public LanguageService(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<ICollection<LanguageDto>> GetLanguageDtosAsync()
    {
        var languages = await _languageRepository.GetAllLanguagesAsync();
        return languages.Select(l => new LanguageDto(l.LanguageName, l.Id)).ToList();
    }
}
