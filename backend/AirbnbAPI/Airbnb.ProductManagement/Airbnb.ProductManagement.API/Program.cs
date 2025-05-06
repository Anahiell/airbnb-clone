using System.Reflection;
using System.Text.Json.Serialization;
using Airbnb.Application.Behaviors;
using Airbnb.Application.Results;
using Airbnb.Connection.ConnectionRealization;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.Infrastructure.Configuration;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Repositories;
using Airbnb.MongoRepository.Configuration;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using Airbnb.ProductManagement.Application.BoundedContext.Queries;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.SharedKernel.ConnectionService.HttpConnection.Logs.TraceIdLogic.Interfaces;
using Airbnb.SharedKernel.Repositories;
using AirbnbAPI.Extensions;
using AirbnbAPI.Middleware;
using FluentValidation;
using FluentValidation.Validators;

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
        builder.Services.AddReddisCacheServices();
        builder.Services.AddMongoDbService(builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>() ??
                                           throw new ApplicationException("MongoDb settings not found."));

        builder.Services.AddTransient<IRepository<DomainProduct>, ProductRepository>();
        builder.Services.AddScoped<IRepository<AddressLegal>, AddressRepository>();
        builder.Services.AddScoped<IRepository<ApartmentType>, ApartmentTypeRepository>();

        builder.Services.AddTransient<IProductDataAggregator, ProductDataAggregator>();
        // Добавляем стандартные сервисы
        builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); 
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddProblemDetails();
        
        // HTTP Connection
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IRouteProvider, RouteProvider>();
        builder.Services.AddScoped<IHttpConnectionService, HttpConnectionService>();

        
        // Конфигурация окружения
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
            .AddEnvironmentVariables();
        builder.Services.AddProblemDetails();

        builder.Services.AddSqlServerServices(
            builder.Configuration.GetSection(builder.Environment.EnvironmentName).Get<SqlServerSettings>()
            ?? throw new NullReferenceException());

        var app = builder.Build();

        // Костыль с заполнением БД
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AirbnbDbContext>();
            app.UseSqlServerMigration(dbContext);
            DataSeeder.Seed(dbContext);
        }

        app.UsePathBase("/product");
        // Настройка HTTP запроса
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
            c.RoutePrefix = "swagger";
        });
        app.UseCors("AllowFrontend");
        app.UseExceptionHandler();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}