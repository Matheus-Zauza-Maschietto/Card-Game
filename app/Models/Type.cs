using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class Type
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public ICollection<CardType> CardTypes { get; set; }
    public ICollection<Card> Cards { get; set; }

    public Type()
    {
        
    }

    public Type(string apiType)
    {
        TypeName = apiType;
    }

}
