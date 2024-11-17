using System.Text.Json;
using app.DTOs;
using app.Enums;
using Confluent.Kafka;

namespace app.BackgroundJobs;

public class DeckConsumer : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;

    public DeckConsumer(IConfiguration configuration)
    {

        var config = new ConsumerConfig
        {
            GroupId = "deck-consumer-group",
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _consumer.Subscribe(Topics.ImportDeckTopic);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);
                if (result == null)
                    continue;    
                
                ProcessDeckResult(result);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Consumo de mensagens Kafka cancelado.");
            }
        }

    }

    private async Task ProcessDeckResult(ConsumeResult<Null, string> result)
    {
        var importDeckMessage = JsonSerializer.Deserialize<ImportDeckKafkaMessage>(result.Message.Value);
            
    }
    
    

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}