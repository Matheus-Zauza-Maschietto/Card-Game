using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Helpers;

public static class CacheConfig
{
    public static void ConfigureRedisCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "RedisInstance";
        });
    }
}
