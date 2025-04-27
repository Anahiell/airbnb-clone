using Airbnb.TagManagement.API.Middleware;

namespace Airbnb.TagManagement.API.Extensions;

public static class ExceptionHandlingServiceExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}