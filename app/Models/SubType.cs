using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class SubType
{
    public int Id { get; set; }
    public string SubTypeName { get; set; }
    public ICollection<CardSubType> CardSubTypes { get; set; }
    public ICollection<Card> Cards { get; set; }

    public SubType()
    {
        
    }

    public SubType(string apiSubType)
    {
        SubTypeName = apiSubType;
    }

}
