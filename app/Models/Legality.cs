using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;
public class Legality
{
    public int Id { get; set; }
    public Card Card { get; set; }
    public Guid CardId { get; set; }
    public string Format { get; set; }
    public string LegalityStatus { get; set; }
}