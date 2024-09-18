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
        ICollection<LanguageDto>? languagesDTOs = await _redisRepository.GetCacheAsync<ICollection<LanguageDto>>("languages");
        if(languagesDTOs is not null)
            return languagesDTOs;

        var languages = await _languageRepository.GetAllLanguagesAsync();
        
        languagesDTOs = languages.Select(l => new LanguageDto(l.LanguageName, l.Id)).ToList();
        _redisRepository.SetCacheAsync("languages", languagesDTOs, TimeSpan.FromHours(1));

        return languagesDTOs;
    }
}
