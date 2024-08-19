using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Integration.Enums;

namespace app.Helper;

public static class HttpClientsConfig
{
    public static void ConfigureHttpClients(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient(HttpClients.MagicTheGathering.ToString(), client =>
        {
            client.BaseAddress = new Uri("https://api.magicthegathering.io/v1/");
        });
    }
}
