using app.Models;

namespace app.DTOs;

public class NotificationKafkaMessage
{
    public string Message { get; set; }
    public string UserEmail { get; set; }
}