using System.Reflection;
using Airbnb.Infrastructure.Configuration;
using Airbnb.Infrastructure.DataContext;
using AirbnbAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173") //URL frontend
        .AllowAnyMethod()
        .AllowAnyHeader());
    
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServerServices(builder.Configuration.GetSection("SqlServerConnection").Get<SqlServerSettings>() 
                                      ?? throw new NullReferenceException());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AirbnbDbContext>();
    app.UseSqlServerMigration(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
