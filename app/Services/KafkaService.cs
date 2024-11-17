using System.Text.Json;
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
    public async Task<DeliveryResult<Null, string>> SendMessage(object message, string topic)
    {
        string messageSerialized = JsonSerializer.Serialize(message);
        DeliveryResult<Null, string> result = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = messageSerialized });
        return result;
    }
}