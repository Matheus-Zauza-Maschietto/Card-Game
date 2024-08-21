using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class DeckCard
{
    public int Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public Guid DeckId { get; set; }
    public Deck Deck { get; set; }
    public bool? IsCommander { get; set; }
}
