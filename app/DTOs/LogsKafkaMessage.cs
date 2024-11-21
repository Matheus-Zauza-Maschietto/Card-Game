using app.Enums;

namespace app.DTOs;

public class LogsKafkaMessage
{
    public string Url { get; set; }
    public string? UserEmail { get; set; }
    public string HttpMethod { get; set; }
}