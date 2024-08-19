using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.DTOs;

public class LegalityDTO
{
    public string? Format { get; set; }

    public string? LegalityStatus { get; set; }

    public LegalityDTO()
    {
        
    }

    public LegalityDTO(Legality legality)
    {
        Format = legality.Format;
        LegalityStatus = legality.LegalityStatus;
    }
}
