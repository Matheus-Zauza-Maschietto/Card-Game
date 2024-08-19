using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace app.Models;

public class User : IdentityUser
{
    public string Role { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public ICollection<Deck> Decks { get; set; }

    public User(string email, string username, string role, string language)
    {
        Email = email;
        UserName = username;
        Role = role;
        Language = new Language(language);
    }

    public User()
    {
        
    }
}
