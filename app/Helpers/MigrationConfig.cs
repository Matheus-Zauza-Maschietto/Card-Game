using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using Microsoft.EntityFrameworkCore;

namespace app.Helpers;

public static class MigrationConfig
{
    public static void ConfigureInitialMigration(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        try{
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
        
    }
}
