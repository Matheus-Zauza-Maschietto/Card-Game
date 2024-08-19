using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class Printing
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CardPrinting> CardPrintings { get; set; }
    public ICollection<Card> Cards { get; set; }

    public Printing()
    {
        
    }

    public Printing(string printingName)
    {
        Name = printingName;
    }
}
