using System.Text.Json.Serialization;
using Airbnb.Connection.ConnectionRealization;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using WebApplication1.Consumers;
using WebApplication1.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Конфигурация
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IRouteProvider, RouteProvider>();

        builder.Services.AddOptions<HttpConnectionSettings>()
            .Bind(builder.Configuration.GetSection("Routes"))
            .ValidateDataAnnotations();
        builder.Services.AddScoped<IHttpConnectionService, HttpConnectionService>();

        // Сервисы
        builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // MassTransit + RabbitMQ
        builder.Services.AddMassTransitConsumers(builder.Configuration);

        // SignalR
        builder.Services.AddSignalR();

        var app = builder.Build();

        // Middleware pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web App API V1");
            c.RoutePrefix = "swagger";
        });

        // Маршруты
        app.MapHub<PropertyHub>("/propertyHub");
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}