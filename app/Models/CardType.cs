using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class CardType
{
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public int TypeId { get; set; }
    public Type Type { get; set; }
}
