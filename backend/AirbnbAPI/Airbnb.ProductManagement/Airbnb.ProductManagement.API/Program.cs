using System.Reflection;
using Airbnb.Infrastructure.Configuration;
using Airbnb.Infrastructure.DataContext;
using AirbnbAPI.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy => policy.WithOrigins("http://localhost:5173") //URL frontend
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
            .AddEnvironmentVariables();

        builder.Services.AddSqlServerServices(
            builder.Configuration.GetSection(builder.Environment.EnvironmentName).Get<SqlServerSettings>()
            ?? throw new NullReferenceException());
        
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AirbnbDbContext>();
            app.UseSqlServerMigration(dbContext);
            DataSeeder.Seed(dbContext);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();
        app.UseCors("AllowFrontend");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}