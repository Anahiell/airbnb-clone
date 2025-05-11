using System.Text.Json.Serialization;
using Airbnb.MongoRepository.Configuration;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagementInfrastructure.Configuration;
using Airbnb.ReviewManagementInfrastructure.DataContext;
using Airbnb.ReviewManagementInfrastructure.Repositories;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagManagement.API.Extensions;

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

        builder.Services.AddMassTransitConsumers(builder.Configuration);
        
        builder.Services.AddTransient<IRepository<DomainReview>, ReviewRepository>();

        // Добавляем стандартные сервисы
        builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));;
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

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AirbnbDbContext>();
            app.UseSqlServerMigration(dbContext);
        }

        // Настройка HTTP запроса
        app.UsePathBase("/review");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/review/swagger/v1/swagger.json", "Review API V1");
            c.RoutePrefix = "swagger";
        });
        app.UseCors("AllowFrontend");
        app.UseExceptionHandler();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}