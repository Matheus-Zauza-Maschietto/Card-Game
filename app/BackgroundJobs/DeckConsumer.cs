using System.Collections.ObjectModel;
using System.Text.Json;
using app.DTOs;
using app.Enums;
using app.Models;
using app.Repositories;
using app.Repositories.Interfaces;
using app.Services;
using Confluent.Kafka;

namespace app.BackgroundJobs;

public class DeckConsumer : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public DeckConsumer(
        IConfiguration configuration, 
        IServiceScopeFactory serviceScopeFactory
        )
    {
        _serviceScopeFactory = serviceScopeFactory;
        var config = new ConsumerConfig
        {
            GroupId = "deck-consumer-group",
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            MaxInFlight = 1,
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _consumer.Subscribe(Topics.ImportDeckTopic);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(() =>
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                if (result == null)
                    continue;
                ProcessDeckResult(result);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Consumo de mensagens Kafka cancelado.");
        }
        finally
        {
            _consumer.Close();
        }
    });
    
    
    private async Task ProcessDeckResult(ConsumeResult<Null, string> result)
    {
        try
        {
            var dependency = GetDependencies();
            var importDeckMessage = JsonSerializer.Deserialize<ImportDeckKafkaMessage>(result.Message.Value);
            IEnumerable<Guid> cardsId = importDeckMessage.ImportCardDTO.Select(p => p.Id);
            ICollection<Card> cardsImported = await dependency.cardService.GetCardsByIdAsync(cardsId);
            await dependency.deckService.ImportCardsAsync(cardsImported, importDeckMessage.CreatedDeckId);
            _consumer.Commit(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private (DeckService deckService, CardService cardService, UserService userService) GetDependencies()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var deckService = scope.ServiceProvider.GetRequiredService<DeckService>();
        var cardService = scope.ServiceProvider.GetRequiredService<CardService>();
        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
        return (deckService, cardService, userService);
    }

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}