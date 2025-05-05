using System.Text.Json.Serialization;
using Airbnb.MongoRepository.Configuration;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagManagement.API.Extensions;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Services;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Infrastructure.Configuration;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Airbnb.UserManagement.Infrastructure.Repositories;
using AirbnbAPI.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Конфигурация окружения
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
            .AddEnvironmentVariables();
        builder.Services.AddProblemDetails();

        // Добавляем расширения для Swagger, MediatR, CORS и других
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddCustomCors();
        builder.Services.AddMediatRServices();
        builder.Services.AddExceptionHandling();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.AddJwtBasedAuth();
        builder.Services.AddReddisCacheServices();
        builder.Services.AddMongoDbService(builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>() ??
                                           throw new ApplicationException("MongoDb settings not found."));

        builder.Services.AddTransient<IRepository<DomainUser>, UserRepository>();

        // Добавляем стандартные сервисы
        builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));;
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddProblemDetails();

        builder.Services.AddSqlServerServices(
            builder.Configuration.GetSection(builder.Environment.EnvironmentName).Get<SqlServerSettings>()
            ?? throw new NullReferenceException());

        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            app.UseSqlServerMigration(dbContext);
        }

        // Настройка HTTP запроса
        app.UsePathBase("/user");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/User/swagger.json", "User API V1");
            c.SwaggerEndpoint("/swagger/Auth/swagger.json", "Auth API V1");
            c.RoutePrefix = "swagger";
        });
        app.UseCors("AllowFrontend");
        app.UseExceptionHandler();
        app.UseAuthorization();
        app.MapControllers();
        app.UseJwtBasedAuth();
        app.UseAuthentication();

        app.Run();
    }
}