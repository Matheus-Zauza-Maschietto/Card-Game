using app.Enums;
using Confluent.Kafka;

namespace app.BackgroundJobs;

public class NotificationConsumer : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SemaphoreSlim _semaphore;
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(
        IConfiguration configuration, 
        IServiceScopeFactory serviceScopeFactory,
        ILogger<NotificationConsumer> logger
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        var config = new ConsumerConfig
        {
            GroupId = ConsumerGroups.Notification,
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            MaxInFlight = 5,
            EnableAutoCommit = false,
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _consumer.Subscribe(Topics.NotificationTopic);
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
                ProcessNotificationResult(result);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Consumo de notificações Kafka cancelado.");
        }
        finally
        {
            _consumer.Close();
        }
    });
    
    
    private async Task ProcessNotificationResult(ConsumeResult<Null, string> result)
    {
        try
        {
            _logger.LogInformation(result.Message.Value);
            _consumer.Commit(result);
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