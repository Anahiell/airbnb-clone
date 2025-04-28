using Airbnb.Cache;
using Airbnb.OrderManagement.Infrastructure.Cache;

namespace Airbnb.OrderManagement.API.Extensions;

public static class RedisCacheExtensions
{
    public static IServiceCollection AddReddisCacheServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}