using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models;

public class Language
{
    public int Id { get; set; }
    public string LanguageName { get; set; }
    public ICollection<User> Users { get; set; }

    public Language()
    {
        
    }

    public Language(int id, string languageName)
    {
        Id = id;
        LanguageName = languageName;
    }
}
