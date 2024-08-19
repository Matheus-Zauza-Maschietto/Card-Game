using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Integration.Models;


namespace app.Models;
public class Legality
{
    public int Id { get; set; }
    public Card Card { get; set; }
    public Guid CardId { get; set; }
    public string Format { get; set; }
    public string LegalityStatus { get; set; }

    public Legality()
    {

    }

    public Legality(ApiLegality apiLegality)
    {
        Format = apiLegality.Format ?? string.Empty;
        LegalityStatus = apiLegality.LegalityStatus ?? string.Empty;
    }

    public Legality(LegalityDTO apiLegality)
    {
        Format = apiLegality.Format ?? string.Empty;
        LegalityStatus = apiLegality.LegalityStatus ?? string.Empty;
    }
}