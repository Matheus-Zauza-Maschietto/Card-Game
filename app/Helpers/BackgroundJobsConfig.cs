using app.BackgroundJobs;

namespace app.Helpers;

public static class BackgroundJobsConfig
{
    public static void ConfigureBackgroundJobs(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<DeckConsumer>();
        builder.Services.AddHostedService<LogConsumer>();
        builder.Services.AddHostedService<NotificationConsumer>();

    }
}