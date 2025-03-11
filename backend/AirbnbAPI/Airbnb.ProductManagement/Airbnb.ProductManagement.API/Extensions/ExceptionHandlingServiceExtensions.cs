using AirbnbAPI.Middleware;

namespace AirbnbAPI.Extensions;

public static class ExceptionHandlingServiceExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}