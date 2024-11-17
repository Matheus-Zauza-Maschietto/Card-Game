using app.Services;

namespace app.Helpers;

public static class KafkaConfig
{
    public static void ConfigureKafka(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<KafkaService>();
    }
}