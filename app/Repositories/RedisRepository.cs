using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using app.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace app.Repositories;

public class RedisRepository : IRedisRepository
{
    private readonly IDistributedCache _cache;
    public RedisRepository(IDistributedCache cache)
    {
        _cache = cache;

    }
    public async Task SetCacheAsync(string key, object value, TimeSpan expiration)
    {
        string serializedObject = JsonSerializer.Serialize(value);
        DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration+expiration,
            SlidingExpiration = expiration
        };
        await _cache.SetStringAsync(key, serializedObject, cacheOptions);
    }
    public async Task<T?> GetCacheAsync<T>(string key)
    {
        string? serializedObject = await _cache.GetStringAsync(key);

        if(serializedObject is null)
            return default;

        return JsonSerializer.Deserialize<T>(serializedObject);
    }
}
