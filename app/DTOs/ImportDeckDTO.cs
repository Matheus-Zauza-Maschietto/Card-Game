using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs;

public class ImportDeckDTO
{
    public string Name { get; set; }
    public Guid CommanderCardId { get; set; }
    public ICollection<ImportCardDTO> CardsDTO { get; set; }
}
