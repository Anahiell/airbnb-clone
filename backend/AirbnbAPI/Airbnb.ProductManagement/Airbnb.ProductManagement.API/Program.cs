using System.Reflection;
using Airbnb.Application.Behaviors;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Infrastructure.Configuration;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
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
        
        builder.Services.AddTransient<IProductRepository, ProductRepository>();

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