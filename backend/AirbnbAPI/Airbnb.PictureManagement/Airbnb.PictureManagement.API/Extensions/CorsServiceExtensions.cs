namespace AirbnbAPI.Extensions;

public static class CorsServiceExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
                policy.WithOrigins("http://localhost:5173") // URL frontend
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        return services;
    }
}