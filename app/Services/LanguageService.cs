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
    private readonly IRedisRepository _redisRepository;
    public LanguageService(ILanguageRepository languageRepository, IRedisRepository redisRepository)
    {
        _languageRepository = languageRepository;
        _redisRepository = redisRepository;
    }

    public async Task<ICollection<LanguageDto>> GetLanguageDtosAsync()
    {
        var languages = await _languageRepository.GetAllLanguagesAsync();
        
        ICollection<LanguageDto>? languagesDTOs = languages.Select(l => new LanguageDto(l.LanguageName, l.Id)).ToList();

        return languagesDTOs;
    }
}
