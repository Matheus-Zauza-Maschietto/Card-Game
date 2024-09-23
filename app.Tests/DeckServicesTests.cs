using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Repositories;
using app.Repositories.Interfaces;
using app.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using StackExchange.Redis;

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
    public DeckServiceFixture()
    {
        DeckRepository = Substitute.For<IDeckRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        Configuration = Substitute.For<IConfiguration>();
        RoleRepository = Substitute.For<IRoleRepository>();
        CardRepository = Substitute.For<ICardRepository>();
        CardApiRepository = Substitute.For<ICardApiRepository>();
        DeckCardRepository = Substitute.For<DeckCardRepository>();
        LanguageRepository = Substitute.For<LanguageRepository>();
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

    public async void GetDecksAsync_GetExistDecksByUserEmail_ReturnDecks()
    {
        
    }
}
