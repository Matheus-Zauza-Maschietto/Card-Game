using app.Enums;
using app.Services;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace app.Helpers;

public static class KafkaConfig
{
    public static void ConfigureKafkaDependency(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<KafkaService>();
    }

    public static void ConfigureKafkaTopics(this WebApplicationBuilder builder)
    {
        IAdminClient adminClient = GetAdminClient(builder.Configuration);
        var metadata = adminClient.GetMetadata(Topics.ImportDeckTopic, TimeSpan.FromSeconds(10));
        var topicsToCreate = GetTopicsSpecifications(
                metadata.Topics.Select(topic => topic.Topic)
            );
        if (!topicsToCreate.Any())
            return;
        
        adminClient.CreateTopicsAsync(topicsToCreate);
        Console.WriteLine($"Tópico Criado: {Topics.ImportDeckTopic}");
    }

    private static IEnumerable<TopicSpecification> GetTopicsSpecifications(IEnumerable<string> existingTopicsNames)
    {
        TopicSpecification importTopic = new TopicSpecification()
        {
            Name = Topics.ImportDeckTopic,
            NumPartitions = 1,
            ReplicationFactor = 1,
        };
        TopicSpecification notificationTopic = new TopicSpecification()
        {
            Name = Topics.NotificationTopic,
            NumPartitions = 1,
            ReplicationFactor = 1,
        };
        TopicSpecification logTopic = new TopicSpecification()
        {
            Name = Topics.LogTopic,
            NumPartitions = 1,
            ReplicationFactor = 1,
        };
        var topics = new[] { importTopic, notificationTopic, logTopic };
        
        return topics.Where(
                topic => !existingTopicsNames.Contains(topic.Name, StringComparer.OrdinalIgnoreCase)
            );
    }
    
    private static IAdminClient GetAdminClient(IConfiguration configuration)
    {
        AdminClientConfig config = new AdminClientConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] };
        return new AdminClientBuilder(config).Build();
    }
}