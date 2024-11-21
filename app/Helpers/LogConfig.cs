namespace app.Helpers;

public static class LogConfig
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(logConfig => logConfig.AddSimpleConsole());
    }
}