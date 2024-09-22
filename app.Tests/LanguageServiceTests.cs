using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;
using app.Repositories.Interfaces;
using app.Services;
using NSubstitute;

namespace app.Tests;

public class LanguageServiceTests
{
    [Fact]
    public async void GetLanguageDtosAsync_GetManyLanguages_ReturnsManyLanguages()
    {
        ILanguageRepository languageRepository = Substitute.For<ILanguageRepository>();
        List<Language> languagesMock = new List<Language>() {
            new Language() { LanguageName = "Portugues" }
        };
        languageRepository.GetAllLanguagesAsync().Returns(languagesMock);
        LanguageService service = new LanguageService(languageRepository, Substitute.For<IRedisRepository>());

        ICollection<LanguageDto> languages = await service.GetLanguageDtosAsync();

        Assert.Equal(languagesMock.First().LanguageName, languages.First().LanguageName);
    }
}
