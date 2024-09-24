using app.DTOs;
using app.Integration.Models;
using app.Models;
using app.Repositories;
using app.Repositories.Interfaces;
using app.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace app.Tests;

public class DeckServiceFixture
{
    public DeckService DeckService { get; private set; }
    public CardService CardService { get; private set; }
    public IDeckCardRepository DeckCardRepository { get; private set; }
    public ILanguageRepository LanguageRepository { get; private set; }
    public IDeckRepository DeckRepository { get; private set; }
    public UserService UserService { get; private set; }
    public JsonWebTokensService JsonWebTokensService { get; private set; }
    public IUserRepository UserRepository { get; private set; }
    public RoleService RoleService { get; private set; }
    public IRoleRepository RoleRepository { get; private set; }   
    public IConfiguration Configuration { get; private set; }   
    public ICardApiRepository CardApiRepository { get; private set; }  
    public ICardRepository CardRepository { get; private set; }  

    public List<ApiMagicCard> OneHundredMagicCards { get; private set; } = [
            new ApiMagicCard() 
            { 
                Name = "Card 1", 
                ManaCost = "2G", 
                Cmc = 3.0, 
                Colors = new List<string?> { "Green" }, 
                ColorIdentity = new List<string?> { "G" },
                Type = "Creature", 
                Types = new List<string?> { "Elf" }, 
                Subtypes = new List<string?> { "Warrior" }, 
                Rarity = "Common", 
                Set = "ZNR", 
                SetName = "Zendikar Rising", 
                Text = "When Card 1 enters the battlefield, draw a card.", 
                Artist = "John Doe", 
                Number = "1", 
                Power = "2", 
                Toughness = "2", 
                Layout = "normal", 
                MultiverseId = "123456", 
                ImageUrl = "https://example.com/card1.jpg", 
                Variations = new List<string?> { "123457" }, 
                ForeignNames = new List<ApiForeignName?> 
                { 
                    new ApiForeignName { Name = "Carta Uno", Language = "Spanish", MultiverseId = 654321 } 
                }, 
                Printings = new List<string?> { "ZNR" }, 
                OriginalText = "Cuando entra al campo de batalla, roba una carta.", 
                OriginalType = "Criatura", 
                Legalities = new List<ApiLegality?> 
                { 
                    new ApiLegality { Format = "Standard", LegalityStatus = "Legal" } 
                }, 
                Id = "abcd-efgh-1234-5678" 
            },
            new ApiMagicCard() 
            { 
                Name = "Card 2", 
                ManaCost = "1U", 
                Cmc = 2.0, 
                Colors = new List<string?> { "Blue" }, 
                ColorIdentity = new List<string?> { "U" },
                Type = "Instant", 
                Types = new List<string?> { "Spell" }, 
                Subtypes = new List<string?> { "Wizardry" }, 
                Rarity = "Uncommon", 
                Set = "M21", 
                SetName = "Core Set 2021", 
                Text = "Counter target spell.", 
                Artist = "Jane Smith", 
                Number = "2", 
                Power = null, 
                Toughness = null, 
                Layout = "normal", 
                MultiverseId = "654321", 
                ImageUrl = "https://example.com/card2.jpg", 
                Variations = new List<string?> { "654322" }, 
                ForeignNames = new List<ApiForeignName?> 
                { 
                    new ApiForeignName { Name = "Carta Dos", Language = "Spanish", MultiverseId = 123456 } 
                }, 
                Printings = new List<string?> { "M21" }, 
                OriginalText = "Contrarresta el hechizo objetivo.", 
                OriginalType = "Instantáneo", 
                Legalities = new List<ApiLegality?> 
                { 
                    new ApiLegality { Format = "Commander", LegalityStatus = "Legal" } 
                }, 
                Id = "ijkl-mnop-9876-5432" 
            },
            new ApiMagicCard() 
            { 
                Name = "Card 3", 
                ManaCost = "3R", 
                Cmc = 4.0, 
                Colors = new List<string?> { "Red" }, 
                ColorIdentity = new List<string?> { "R" },
                Type = "Sorcery", 
                Types = new List<string?> { "Fire" }, 
                Subtypes = new List<string?> { "Dragon" }, 
                Rarity = "Rare", 
                Set = "ELD", 
                SetName = "Throne of Eldraine", 
                Text = "Deal 3 damage to any target.", 
                Artist = "Robert Brown", 
                Number = "3", 
                Power = null, 
                Toughness = null, 
                Layout = "normal", 
                MultiverseId = "987654", 
                ImageUrl = "https://example.com/card3.jpg", 
                Variations = new List<string?> { "987655" }, 
                ForeignNames = new List<ApiForeignName?> 
                { 
                    new ApiForeignName { Name = "Carta Tres", Language = "Spanish", MultiverseId = 321654 } 
                }, 
                Printings = new List<string?> { "ELD" }, 
                OriginalText = "Haz 3 de daño a cualquier objetivo.", 
                OriginalType = "Conjuro", 
                Legalities = new List<ApiLegality?> 
                { 
                    new ApiLegality { Format = "Modern", LegalityStatus = "Legal" } 
                }, 
                Id = "qrst-uvwx-1122-3344" 
            },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
            new ApiMagicCard(){ Name = "Carta 1" },
        ];

    public DeckServiceFixture()
    {
        DeckRepository = Substitute.For<IDeckRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        Configuration = Substitute.For<IConfiguration>();
        RoleRepository = Substitute.For<IRoleRepository>();
        CardRepository = Substitute.For<ICardRepository>();
        CardApiRepository = Substitute.For<ICardApiRepository>();
        DeckCardRepository = Substitute.For<IDeckCardRepository>();
        LanguageRepository = Substitute.For<ILanguageRepository>();
        JsonWebTokensService = new JsonWebTokensService(Configuration);
        CardService = new CardService(
            CardRepository,
            CardApiRepository
        );
        RoleService = new RoleService(
            RoleRepository
        );
        UserService = new UserService(
            JsonWebTokensService,
            UserRepository,
            RoleService
        );

        DeckService = new DeckService(
            DeckRepository,
            UserService,
            CardService,
            DeckCardRepository,
            LanguageRepository
        );
    }
}

public class DeckServicesTests : IClassFixture<DeckServiceFixture>
{
    private readonly DeckServiceFixture _fixture;
    public DeckServicesTests(DeckServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetDecksAsync_GetExistDecksByUserEmail_ReturnDecks()
    {
        User foundedUser = new User(){
            Email = "testedecks@teste.com"
        };
        List<Deck> decksToBeReturned = new List<Deck>(){
            new Deck(){ Id = Guid.NewGuid(), Name = "Deck1" },
            new Deck(){ Id = Guid.NewGuid(), Name = "Deck2" },
        };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);
        _fixture.DeckRepository.GetAllDecksByUserAsync(foundedUser).Returns(decksToBeReturned);

        List<Deck> decks = (await _fixture.DeckService.GetDecksAsync("testedecks@teste.com")).ToList();
        Assert.Equal(decksToBeReturned[0].Id, decks[0].Id);
        Assert.Equal(decksToBeReturned[0].Name, decks[0].Name);
        Assert.Equal(decksToBeReturned[1].Id, decks[1].Id);
        Assert.Equal(decksToBeReturned[1].Name, decks[1].Name);   
    }

    [Fact]
    public async void GetDecksByIdAsync_GetNotExistDeckByUserEmailAndId_ThrowsException()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testedeckunico@teste.com"
        };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await _fixture.DeckService.GetDecksByIdAsync("testedeckunico@teste.com", id));

        Assert.Equal("Deck not found", exception.Message);
    }


    [Fact]
    public async void GetDecksByIdAsync_GetExistDeckByUserEmailAndId_ReturnDeck()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testedeckunico@teste.com"
        };
        Deck deckToBeReturned = new Deck(){ Id = Guid.NewGuid(), Name = "Deck unico", Cards = [
            new Card(){ Name = "Carta 1" },
            new Card(){ Name = "Carta 2" },
        ] };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);
        _fixture.DeckRepository.GetDeckByUserAndIdUsingIncludesAsync(foundedUser, id).Returns(deckToBeReturned);

        Deck? deck = await _fixture.DeckService.GetDecksByIdAsync("testedeckunico@teste.com", id);

        Assert.Equal(deckToBeReturned.Name, deck.Name);
        Assert.Equal(deckToBeReturned.Cards.First().Name, deck.Cards.First().Name);
        Assert.Equal(deckToBeReturned.Cards.Skip(1).First().Name, deck.Cards.Skip(1).First().Name);
    }

    [Fact]
    public async void UpdateDeckNameByIdAsync_GetExistDeck_ChangeDeckName()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testeDeAlteracao@teste.com"
        };
        Deck deckToBeReturned = new Deck(){ Id = Guid.NewGuid(), Name = "Nome que sera alterado" };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);
        _fixture.DeckRepository.GetDeckByUserAndIdAsync(foundedUser, id).Returns(deckToBeReturned);
        _fixture.DeckRepository.UpdateDeckAsync(deckToBeReturned).Returns(new Deck(){ Id = Guid.NewGuid(), Name = "Nome alterado" });

        Deck? deck = await _fixture.DeckService.UpdateDeckNameByIdAsync("testeDeAlteracao@teste.com", id, "Nome alterado");

        Assert.Equal("Nome alterado", deck.Name);
    }

    [Fact]
    public async void UpdateDeckNameByIdAsync_GetNotExistDeck_ThrowsException()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testeDeAlteracao@teste.com"
        };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await _fixture.DeckService.UpdateDeckNameByIdAsync("testeDeAlteracao@teste.com", id, "Nome alterado"));

        Assert.Equal("Deck not found", exception.Message);
    }

    [Fact]
    public async void DeleteDeckByIdAsync_GetNotExistDeck_ThrowsException()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testeDeAlteracao@teste.com"
        };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await _fixture.DeckService.DeleteDeckByIdAsync("testeDeAlteracao@teste.com", id));

        Assert.Equal("Deck not found", exception.Message);
    }

    [Fact]
    public async void DeleteDeckByIdAsync_GetExistDeck_ReturnDeletedDeck()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testeDeAlteracao@teste.com"
        };
        Deck deckToBeReturned = new Deck(){ Id = Guid.NewGuid(), Name = "DeckDeletado" };
        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);
        _fixture.DeckRepository.GetDeckByUserAndIdAsync(foundedUser, id).Returns(deckToBeReturned);
        _fixture.DeckRepository.DeleteDeckAsync(deckToBeReturned).Returns(new Deck(){ Id = Guid.NewGuid(), Name = "Deleted Deck"});

        Deck? deck = await _fixture.DeckService.DeleteDeckByIdAsync("testeDeAlteracao@teste.com", id);

        Assert.Equal("Deleted Deck", deck.Name);
    }

    [Fact]
    public async void CreateDeckAsync_CreateDeckByCommanderId_ReturnCreatedDeck()
    {
        Guid id = Guid.NewGuid();
        User foundedUser = new User(){
            Email = "testeDeCriacao@teste.com"
        };
        CreateDeckDTO deckDTO = new CreateDeckDTO() { Name = "Meu deck", CommanderCardId = id };
        Card cardCommander = new Card() { Id = id, Name = "Card commander", Colors = [ new Color(){ ColorAbbreviation = "g" } ] };

        _fixture.UserRepository.GetUserByEmail(foundedUser.Email).Returns(foundedUser);
        _fixture.CardRepository.GetCardByIdAsync(id).Returns(cardCommander);
        _fixture.CardApiRepository.GetOneHundredCardsByColorAsync(
            cardCommander.Colors.First().ColorAbbreviation.First()
        ).Returns(_fixture.OneHundredMagicCards);
        _fixture.DeckRepository.CreateDeckAsync(
            new Deck()).ReturnsForAnyArgs(new Deck() { DeckCards = [ new DeckCard() { Card = new Card() { Id = id } } ] }
        );

        Deck deck = await _fixture.DeckService.CreateDeckAsync("testeDeCriacao@teste.com", deckDTO);
        Assert.Equal(deckDTO.Name, deck.Name);
    }
}
