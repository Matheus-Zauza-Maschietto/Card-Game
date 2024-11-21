using app.Models;

namespace app.DTOs;

public class ImportDeckKafkaMessage
{
    public ImportCardDTO ImportCardDTO { get; set; }
    public string UserId { get; set; }
    public Guid CreatedDeckId { get; set; }
    
}