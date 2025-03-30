using Airbnb.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Airbnb.Infrastructure.Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var result = await _memoryCache.GetOrCreateAsync(key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

                return factory(cancellationToken);
            });

        return result;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}