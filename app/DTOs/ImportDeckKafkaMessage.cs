using app.Models;

namespace app.DTOs;

public class ImportDeckKafkaMessage
{
    public ICollection<ImportCardDTO> ImportCardDTO { get; set; }
    public User User { get; set; }
    public Deck CreatedDeck { get; set; }
    
}