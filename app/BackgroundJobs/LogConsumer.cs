using app.Enums;
using Confluent.Kafka;

namespace app.BackgroundJobs;

public class LogConsumer : BackgroundService
{
    private readonly ILogger<LogConsumer> _logger;
    private readonly IConsumer<Null, string> _consumer;
    private readonly SemaphoreSlim _semaphore;
    public LogConsumer( 
            IConfiguration configuration, 
            ILogger<LogConsumer> logger
        )
    {
        var config = new ConsumerConfig
        {
            GroupId = ConsumerGroups.Log,
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            MaxInFlight = 5,
            EnableAutoCommit = false,
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _consumer.Subscribe(Topics.LogTopic);
        _semaphore = new SemaphoreSlim(5);
        _logger = logger;
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
                _logger.LogInformation(result.Message.Value);
                _consumer.Commit(result);
                _semaphore.Release();
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
    
    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}