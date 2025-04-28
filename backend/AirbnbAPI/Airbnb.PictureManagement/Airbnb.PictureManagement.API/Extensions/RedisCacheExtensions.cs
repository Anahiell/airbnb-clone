using Airbnb.Cache;
using Airbnb.PictureManagement.Infrastructure.Cache;

namespace AirbnbAPI.Extensions;

public static class RedisCacheExtensions
{
    public static IServiceCollection AddReddisCacheServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}