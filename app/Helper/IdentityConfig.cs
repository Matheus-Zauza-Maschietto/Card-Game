using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Models;
using Microsoft.AspNetCore.Identity;

namespace app.Helper;

public static class IdentityConfig
{
    public static void ConfigureIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
        }).AddEntityFrameworkStores<ApplicationDbContext>(); ;

    }
}
