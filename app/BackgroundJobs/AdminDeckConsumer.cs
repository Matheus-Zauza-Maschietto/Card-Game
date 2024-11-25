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

public class AdminDeckConsumer : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SemaphoreSlim _semaphore;

    public AdminDeckConsumer(
        IConfiguration configuration, 
        IServiceScopeFactory serviceScopeFactory
        )
    {
        _serviceScopeFactory = serviceScopeFactory;
        var config = new ConsumerConfig
        {
            GroupId = ConsumerGroups.AdminDeckImport,
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            MaxInFlight = 2,
            EnableAutoCommit = false,
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _consumer.Subscribe(Topics.ImportAdminDeckTopic);
        _semaphore = new SemaphoreSlim(2);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(async () =>
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                if (result == null)
                    continue;
                await _semaphore.WaitAsync(stoppingToken);
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
            using var scope = _serviceScopeFactory.CreateScope();
            var deckService = scope.ServiceProvider.GetRequiredService<DeckService>();
            var cardService = scope.ServiceProvider.GetRequiredService<CardService>();
            var importDeckMessage = JsonSerializer.Deserialize<ImportDeckKafkaMessage>(result.Message.Value);
            Card cardsImported = await cardService.GetCardByIdAsync(importDeckMessage.ImportCardDTO.Id);
            await deckService.ImportCardAsync(cardsImported, importDeckMessage.CreatedDeckId);
            _consumer.Commit(result);
            scope.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}