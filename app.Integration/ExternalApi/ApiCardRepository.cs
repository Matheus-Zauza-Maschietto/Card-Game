using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using app.Integration.DTOs;
using app.Integration.Enums;
using app.Integration.Models;
using app.Repositories.Interfaces;

namespace app.Repositories;

public class CardApiRepository :  ICardApiRepository
{
    private readonly HttpClient _magicHttpClient;
    public CardApiRepository(IHttpClientFactory factory)
    {
        _magicHttpClient = factory.CreateClient(HttpClients.MagicTheGathering.ToString());
    }

    public async Task<ICollection<ApiMagicCard>?> GetOneHundredCardsAsync(int page)
    {
        HttpResponseMessage response = await _magicHttpClient.GetAsync($"cards?page={page}&pageSize=100&random=true");
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        MagicCardListResponse? cards = JsonSerializer.Deserialize<MagicCardListResponse>(content);
        return cards?.cards;
    }

    public async Task<ApiMagicCard?> GetMagicCardByIdAsync(Guid id)
    {
        HttpResponseMessage response = await _magicHttpClient.GetAsync($"cards/{id}");
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        MagicCardResponse? card = JsonSerializer.Deserialize<MagicCardResponse>(content);
        return card?.card;
    }

    public async Task<ICollection<ApiMagicCard>?> GetOneHundredCardsByColorAsync(char color)
    {
        HttpResponseMessage response = await _magicHttpClient.GetAsync($"cards?colors={color}&random=true&pageSize=100");
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        MagicCardListResponse? card = JsonSerializer.Deserialize<MagicCardListResponse>(content);
        return card?.cards;
    }
}
