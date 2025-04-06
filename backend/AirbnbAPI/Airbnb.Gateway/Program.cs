using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

//  JWT-заглушка (фейковый секрет, временно)
var fakeSecret = "super_secret_development_key_only_for_testing";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fakeSecret))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// Конфигурация окружения и загрузка настроек
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();
builder.Services.AddProblemDetails();

var configFile = $"appsettings.{builder.Environment.EnvironmentName}.json";
Log.Information("Loading config file: {ConfigFile}", configFile);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Получаем настройки ReverseProxy
var reverseProxySection = builder.Configuration.GetSection("ReverseProxy");
var clustersSection = reverseProxySection.GetSection("Clusters");
var productCluster = clustersSection.GetSection("productCluster");
var destinationsSection = productCluster.GetSection("Destinations");
var productSection = destinationsSection.GetSection("product");

string productAddress = productSection.GetValue<string>("Address");

if (string.IsNullOrEmpty(productAddress))
{
    productAddress = "http://localhost:8080/";
}

// Логируем полученный адрес продукта
Log.Information($"Product address: {productAddress}");

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();

app.UseCors("AllowAll");

// Логирование запросов и ответов прокси
app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.Use(async (context, next) =>
    {
        var path = context.Request.Path;
        var method = context.Request.Method;
        var queryString = context.Request.QueryString;
    
        Log.Information($"[Proxy] Incoming request: {method} {path}{queryString}");

        if (!context.Request.Headers.TryGetValue("X-Forwarded-For", out _))
        {
            context.Request.Headers["X-Forwarded-For"] = context.Connection.RemoteIpAddress?.ToString();
        }

        try
        {
            await next.Invoke();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "[Proxy] Error forwarding request");
        }

        var statusCode = context.Response.StatusCode;
        Log.Information($"[Proxy] Response status code: {statusCode}");
    });
});

// Логирование конфигурации прокси
var proxyConfig = builder.Configuration.GetSection("ReverseProxy").GetChildren();
foreach (var section in proxyConfig)
{
    Log.Information("[Proxy Config] {Key}: {Value}", section.Key, section.Value);
}

var routeBuilder = app.Services.GetRequiredService<EndpointDataSource>();
foreach (var endpoint in routeBuilder.Endpoints)
{
    Log.Information("[Routes] {Endpoint}", endpoint.DisplayName);
}

app.Run();
