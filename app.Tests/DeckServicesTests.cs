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

namespace app.Tests;

public class DeckServiceFixture
{
    public DeckService DeckService { get; private set; }
    public CardService CardService { get; private set; }
    public IDeckCardRepository IDeckCardRepository { get; private set; }
    public ILanguageRepository ILanguageRepository { get; private set; }
    public IDeckRepository IDeckRepository { get; private set; }
    public UserService UserService { get; private set; }
    public JsonWebTokensService JsonWebTokensService { get; private set; }
    public IUserRepository IUserRepository { get; private set; }
    public RoleService RoleService { get; private set; }
    public RoleManager<IdentityRole> RoleManager { get; private set; }    

    public DeckServiceFixture()
    {
        IDeckRepository deckRepository = Substitute.For<IDeckRepository>();
        UserService userService = new UserService(
            new JsonWebTokensService(Substitute.For<IConfiguration>()),


        );
        DeckService deckService = new DeckService(
            deckRepository,
            userRepository,
            Substitute.For<CardService>(),
            Substitute.For<IDeckCardRepository>(),
            Substitute.For<ILanguageRepository>()
        );
    }
}

public class DeckServicesTests : IClassFixture<DeckServiceFixture>
{
    public async void GetDecksAsync_GetExistDecksByUserEmail_ReturnDecks()
    {

    }
}
