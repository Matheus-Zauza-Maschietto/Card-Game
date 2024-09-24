using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using NATS.Client.JetStream;
using NBomber.CSharp;

using HttpClient httpClient = new HttpClient();

HttpResponseMessage response = await LoginWithUser();
if(!response.IsSuccessStatusCode)
{
    await CreateUser();
    response = await LoginWithUser();
}

string token = await response.Content.ReadAsStringAsync();
httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {token}");

HttpResponseMessage responseCard = await CreateDeck(token);
string content = await responseCard.Content.ReadAsStringAsync();
DeckDTO? deckCreated = JsonSerializer.Deserialize<DeckDTO>(content);

var scenario = Scenario.Create("Teste de carga de endpoint de busca de deck por id", async context =>
    {
        var response = await httpClient.GetAsync($"http://localhost:5000/api/Deck/{deckCreated.id}");

        return response.IsSuccessStatusCode
            ? Response.Ok()
            : Response.Fail();
    })
    .WithoutWarmUp()
    .WithLoadSimulations(
        Simulation.Inject(rate: 250,
            interval: TimeSpan.FromSeconds(0.5),
            during: TimeSpan.FromMinutes(1))
    );
    
    NBomberRunner
        .RegisterScenarios(scenario)
        .Run();

async Task<HttpResponseMessage> LoginWithUser()
{
    return await httpClient.PostAsync("http://localhost:5000/api/User/Login", new StringContent(JsonSerializer.Serialize(
        new LoginUserDTO("string@string.com", "string")
    ), new MediaTypeHeaderValue("application/json")));
}

async Task<HttpResponseMessage> CreateUser()
{
    return await httpClient.PostAsync("http://localhost:5000/api/User/Create", new StringContent(JsonSerializer.Serialize(
        new CreateUserDTO("string@string.com", "string", 7, "string", false)
    ), new MediaTypeHeaderValue("application/json")));
}

async Task<HttpResponseMessage> CreateDeck(string userToken)
{
    HttpRequestMessage message = new HttpRequestMessage();
    message.Content = new StringContent(JsonSerializer.Serialize(
        new {
                name = "deck de teste de carga",
                commanderCardId = "5f8287b1-5bb6-5f4c-ad17-316a40d5bb0c"
            }
    ), new MediaTypeHeaderValue("application/json"));
    message.Method = HttpMethod.Post;
    message.RequestUri = new Uri("http://localhost:5000/api/Deck");
    message.Headers.TryAddWithoutValidation("Authorization", "bearer "+userToken);
    return await httpClient.SendAsync(message);
}

public record class DeckDTO(Guid id);
public record LoginUserDTO(string Email, string Password);
public record CreateUserDTO(string Email, string UserName, int LanguageId, string Password, bool IsAdmin);