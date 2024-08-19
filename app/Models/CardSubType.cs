using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class CardSubType
{
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public int TypeId { get; set; }
    public SubType Type { get; set; }
}
