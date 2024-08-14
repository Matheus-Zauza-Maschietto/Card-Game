using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class Color
{
    public int Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public string ColorAbbreviation { get; set; }
}
