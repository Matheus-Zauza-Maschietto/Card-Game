using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Repositories.Interfaces;
using app.Services;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace app.Tests;

public class CardServiceTest
{
    private readonly ICardRepository _cardRepository;
    private readonly ICardApiRepository _cardApiRepository;
    public CardServiceTest()
    {
        _cardRepository = Substitute.For<ICardRepository>();
        _cardApiRepository = Substitute.For<ICardApiRepository>();
    }

    public static IEnumerable<object[]> GetCardByIdAsync_FoundCenarios_Data(){
        List<object[]> data = new ();
        Card card = new Card(){
            
        };

        return data;
    }

    [Theory]
    [MemberData(nameof(GetCardByIdAsync_FoundCenarios_Data))]
    public async void GetCardByIdAsync_FoundingCardsByDiferentsRespositories_FoundCenarios(CardService cardService, Guid id, Card card)
    {
        Card cardFounded;
        cardFounded = await cardService.GetCardByIdAsync(id);
        Assert.Equal(cardFounded, card);
    }
}
