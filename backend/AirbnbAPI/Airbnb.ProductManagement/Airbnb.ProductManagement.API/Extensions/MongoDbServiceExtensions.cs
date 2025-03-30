using Airbnb.MongoRepository.Configuration;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MongoDB.Driver;

namespace AirbnbAPI.Extensions;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDbService(this IServiceCollection services, MongoDbSettings settings)
    {
        services.AddSingleton(x =>
            new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Url}:{settings.Port}/"));
        services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(settings.Database));

        services.AddTransient<BaseMongoRepository<ProductEntityInfo>, MongoDbRepository<ProductEntityInfo>>();
        services.AddTransient<IProjectionRepository<ProductEntityInfo>, MongoDbRepository<ProductEntityInfo>>();

        return services;
    }
}