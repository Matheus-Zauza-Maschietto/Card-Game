using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace app.Models;

public class User : IdentityUser
{
    public string Role { get; set; }
}
