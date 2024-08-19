using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class CardPrinting
{
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public int PrintingId { get; set; }
    public Printing Printing { get; set; }
}
