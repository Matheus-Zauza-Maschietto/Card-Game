using app.Models;

namespace app.DTOs;

public class ImportDeckKafkaMessage
{
    public ImportCardDTO ImportCardDTO { get; set; }
    public string UserId { get; set; }
    public Guid CreatedDeckId { get; set; }

    public ImportDeckKafkaMessage()
    {
        
    }

    public ImportDeckKafkaMessage(ImportCardDTO importCardDTO, User user, Deck createdDeck)
    {
        ImportCardDTO = importCardDTO;
        UserId = user.Id;
        CreatedDeckId = createdDeck.Id;
    }
    
}