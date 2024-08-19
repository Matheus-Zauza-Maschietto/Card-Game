using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class CardColor
{
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public int ColorId { get; set; }
    public Color Color { get; set; }   
}
