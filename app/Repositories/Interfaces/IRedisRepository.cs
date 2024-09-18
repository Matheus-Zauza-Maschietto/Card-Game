using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Repositories.Interfaces;

public interface IRedisRepository
{
    Task SetCacheAsync(string key, object value, TimeSpan expiration);
    Task<T?> GetCacheAsync<T>(string key);
}
