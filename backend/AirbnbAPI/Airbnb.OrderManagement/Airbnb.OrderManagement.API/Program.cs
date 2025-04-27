using Airbnb.MongoRepository.Configuration;
using Airbnb.OrderManagement.API.Extensions;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Infrastructure.Configuration;
using Airbnb.OrderManagement.Infrastructure.Repositories;
using Airbnb.SharedKernel.Repositories;
using AirbnbAPI.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Добавляем расширения для Swagger, MediatR, CORS и других
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddCustomCors();
        builder.Services.AddMediatRServices();
        builder.Services.AddExceptionHandling();
        builder.Services.AddMemoryCache();
        builder.Services.AddMongoDbService(builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>() ??
                                           throw new ApplicationException("MongoDb settings not found."));

        builder.Services.AddTransient<IRepository<DomainOrder>, OrderRepository>();

        // Добавляем стандартные сервисы
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddProblemDetails();

        // Конфигурация окружения
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
            .AddEnvironmentVariables();
        builder.Services.AddProblemDetails();

        builder.Services.AddPostgreSqlServices(
            builder.Configuration.GetSection(builder.Environment.EnvironmentName).Get<PostgreSqlSettings>()
            ?? throw new NullReferenceException());

        var app = builder.Build();


        // Настройка HTTP запроса
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("AllowFrontend");
        app.UseExceptionHandler();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}