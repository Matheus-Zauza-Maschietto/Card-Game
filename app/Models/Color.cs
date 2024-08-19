using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class Color
{
    public int Id { get; set; }
    public string ColorAbbreviation { get; set; }
    public ICollection<CardColor> CardColors { get; set; }
    public ICollection<Card> Cards { get; set; }

    public Color()
    {

    }

    public Color(string apiColor)
    {
        ColorAbbreviation = apiColor;
    }
}
