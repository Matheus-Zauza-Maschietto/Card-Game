using System.Text.Json;
using app.DTOs;
using app.Enums;
using Confluent.Kafka;

namespace app.Services;

public class KafkaService
{
    private readonly IProducer<Null, string> _producer;
    public KafkaService(IConfiguration configuration)
    {
        var config = new ProducerConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }
    public async Task<DeliveryResult<Null, string>> SendMessage<T>(T message, string topic)
    {
        string messageSerialized = JsonSerializer.Serialize(message);
        DeliveryResult<Null, string> result = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = messageSerialized });
        return result;
    }

    public async Task<DeliveryResult<Null, string>> SendNotification(NotificationKafkaMessage message) =>
        await SendMessage(message, Topics.NotificationTopic);
    
    
    public async Task<DeliveryResult<Null, string>> SendLog(LogsKafkaMessage message) =>
        await SendMessage(message, Topics.LogTopic);

    public async Task<DeliveryResult<Null, string>> SendImportation(ImportDeckKafkaMessage message) =>
        await SendMessage(message, Topics.ImportDeckTopic);
    
    public async Task<DeliveryResult<Null, string>> SendAdminImportation(ImportDeckKafkaMessage message) =>
        await SendMessage(message, Topics.ImportAdminDeckTopic);
}