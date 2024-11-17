using app.DTOs;
using app.Models;
using app.Repositories.Interfaces;
using app.Services;
using NSubstitute;

namespace app.Tests;

public class LanguageServiceFixture 
{
    public ILanguageRepository ILanguageRepository { get; private set; }    
    public LanguageServiceFixture()
    {
        ILanguageRepository = Substitute.For<ILanguageRepository>();
    }
}

public class LanguageServiceTests : IClassFixture<LanguageServiceFixture>
{
    private readonly LanguageServiceFixture _fixture;
    public LanguageServiceTests(LanguageServiceFixture fixture)
    {
        _fixture = fixture;    
    }

    [Fact]
    public async void GetLanguageDtosAsync_GetManyLanguages_ReturnsManyLanguages()
    {
        List<Language> languagesMock = new List<Language>() {
            new Language() { LanguageName = "Portugues" }
        };
        _fixture.ILanguageRepository.GetAllLanguagesAsync().Returns(languagesMock);
        LanguageService service = new LanguageService(_fixture.ILanguageRepository);

        ICollection<LanguageDto> languages = await service.GetLanguageDtosAsync();

        Assert.Equal(languagesMock.First().LanguageName, languages.First().LanguageName);
    }
}
