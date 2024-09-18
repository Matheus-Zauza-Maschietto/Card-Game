using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace app.Models;

public class User : IdentityUser
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public ICollection<Deck> Decks { get; set; }

    public User(string email, string username, int languageId)
    {
        Email = email;
        UserName = username;
        LanguageId = languageId;
    }

    public User()
    {
        
    }
}
