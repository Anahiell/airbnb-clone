using Airbnb.Cache;
using Airbnb.UserManagement.Infrastructure.Caching;

namespace Airbnb.TagManagement.API.Extensions;

public static class RedisCacheExtensions
{
    public static IServiceCollection AddReddisCacheServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}