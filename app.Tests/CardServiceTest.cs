using System.Text.Json;
using app.DTOs;
using app.Integration.Models;
using app.Models;
using app.Repositories;
using app.Repositories.Interfaces;
using app.Services;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace app.Tests;

public class CardServiceTest
{


    [Fact]
    public async void GetCardByIdAsync_FoundCardAtDataBase_FoundCard()
    {
        Card cardToBeFound = new Card(){
            Id = Guid.NewGuid()
        };
        ICardRepository cardRepository = Substitute.For<ICardRepository>();
        cardRepository.GetCardByIdAsync(cardToBeFound.Id).Returns(cardToBeFound);
        CardService cardService = new CardService(cardRepository, Substitute.For<ICardApiRepository>());

        Card? foundedCard = await cardService.GetCardByIdAsync(cardToBeFound.Id);
        Assert.Equal(cardToBeFound, foundedCard);
    }

    [Fact]
    public async void GetCardByIdAsync_FoundCardAtApi_FoundCard()
    {
        Guid id = Guid.NewGuid();
        ApiMagicCard cardFromApi = new ApiMagicCard(){
            Id = id.ToString(),
            Name = "Carta teste"
        };
        Card cardCreatedFromApi = new Card(cardFromApi);
        ICardApiRepository cardApiRepository = Substitute.For<ICardApiRepository>();
        cardApiRepository.GetMagicCardByIdAsync(id).Returns(cardFromApi);
        ICardRepository cardRepository = Substitute.For<ICardRepository>();
        cardRepository.CreateCard(cardCreatedFromApi).ReturnsForAnyArgs(cardCreatedFromApi);
        CardService cardService = new CardService(cardRepository, cardApiRepository);

        Card? foundedCard = await cardService.GetCardByIdAsync(id);
        
        Assert.Equal(cardCreatedFromApi.Name, foundedCard?.Name);
    }

    [Fact]
    public async void GetCardByIdAsync_FoundCardAtApi_ThowsException()
    {
        CardService cardService = new CardService(Substitute.For<ICardRepository>(), Substitute.For<ICardApiRepository>());
        Guid id = Guid.NewGuid();

        Exception exception = await Assert.ThrowsAnyAsync<Exception>(async () => await cardService.GetCardByIdAsync(id));
        Assert.Equal("Card not found", exception.Message);
    }

    [Fact]
    public async void CreateCardAsync_CreateValidCard_ReturnCreatedCard()
    {
        Card cardToBeCreated = new Card(){
            Id = Guid.NewGuid()
        };
        ICardRepository cardRepository = Substitute.For<ICardRepository>();
        cardRepository.CreateCard(cardToBeCreated).Returns(cardToBeCreated);
        CardService cardService = new CardService(cardRepository, Substitute.For<ICardApiRepository>());

        Card cardCreated = await cardService.CreateCardAsync(cardToBeCreated);

        Assert.Equal(cardCreated, cardToBeCreated);
    }

    [Fact]
    public void SetCardLanguage_SetValidLanguage_ReturnCardWithCorrectLanguage()
    {
        Language language = new Language(){
            Id = 7,
            LanguageName = "Portuguese (Brazil)",
        };
        ForeignName foreignNameAtCard = new ForeignName(){
            Name = "Eleito da Ancestral",
            Text = "Iniciativa (Esta criatura causa dano de combate antes de criaturas sem a habilidade de iniciativa.)\nQuando Eleito da Ancestral entra em jogo, você ganha 1 ponto de vida para cada card em seu cemitério.",
            Type = "Criatura — Humano Clérigo",
            Flavor = "\"A vontade de todos pelas minhas mãos realizada.\"",
            ImageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=149551&type=card",
            Language = "Portuguese (Brazil)",
            ScryfallId = "fd447049-4cd9-4850-a7c0-6051f9672710",
            MultiverseId = 149551
        };
        Card cardToBeTranslated = new Card(){
            ForeignNames = new List<ForeignName>() {
                foreignNameAtCard
            },
            Colors = new List<Color>() { new Color(){ ColorAbbreviation = "R" } },
            Legalities = new List<Legality>() { new Legality() { LegalityStatus = "ok" } },
            Subtypes = new List<SubType>() { new SubType() { SubTypeName = "teste" } },
            Types = new List<app.Models.Type>() { new app.Models.Type() { TypeName = "teste" } },
            Printings = new List<Printing>() { new Printing() { Name = "teste" } }
        };
        CardService cardService = new CardService(Substitute.For<ICardRepository>(), Substitute.For<ICardApiRepository>());

        CardDTO cardTranslatedDTO = cardService.SetCardLanguage(cardToBeTranslated, language);

        Assert.Equal(foreignNameAtCard.Text, cardTranslatedDTO.Text);
        Assert.Equal(foreignNameAtCard.Type, cardTranslatedDTO.Type);
        Assert.Equal(foreignNameAtCard.ImageUrl, cardTranslatedDTO.ImageUrl);
        Assert.Equal(foreignNameAtCard.Name, cardTranslatedDTO.Name);
    }

    [Fact]
    public void SetCardLanguage_SetInvalidLanguage_ReturnCardNotTranslated()
    {
        Language language = new Language(){
            Id = 7,
            LanguageName = "English",
        };
        Card cardToBeTranslated = new Card(){
            Name = "Eleito da Ancestral",
            Text = "Iniciativa (Esta criatura causa dano de combate antes de criaturas sem a habilidade de iniciativa.)\nQuando Eleito da Ancestral entra em jogo, você ganha 1 ponto de vida para cada card em seu cemitério.",
            Type = "Criatura — Humano Clérigo",
        };
        CardService cardService = new CardService(Substitute.For<ICardRepository>(), Substitute.For<ICardApiRepository>());

        CardDTO cardTranslatedDTO = cardService.SetCardLanguage(cardToBeTranslated, language);

        Assert.Equal(cardToBeTranslated.Text, cardTranslatedDTO.Text);
        Assert.Equal(cardToBeTranslated.Type, cardTranslatedDTO.Type);
        Assert.Equal(cardToBeTranslated.Name, cardTranslatedDTO.Name);
    }

    [Fact]
    public async void GetOneHundredCardsByColorAsync_GetFirstColorWithNoColorCard_ThorwsException()
    {
        Card paramCard = new Card();
        CardService cardService = new CardService(Substitute.For<ICardRepository>(), Substitute.For<ICardApiRepository>());

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await cardService.GetOneHundredCardsByColorAsync(paramCard));
        Assert.Equal("Color not found", exception.Message);
    }

    [Fact]
    public async void GetOneHundredCardsByColorAsync_NoCardsFoundAtApi_ThorwsException()
    {
        Color color = new Color(){ ColorAbbreviation = "r" };
        Card paramCard = new Card(){
            Colors = new List<Color>(){
                color
            }
        };
        ICardApiRepository cardApiRepository = Substitute.For<ICardApiRepository>();
        CardService cardService = new CardService(Substitute.For<ICardRepository>(), cardApiRepository);

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await cardService.GetOneHundredCardsByColorAsync(paramCard));
        Assert.Equal("Cards not found", exception.Message);
    }
    
}
