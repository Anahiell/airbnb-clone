using Airbnb.MongoRepository.Configuration;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.TagManagement.API.Extensions;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDbService(this IServiceCollection services, MongoDbSettings settings)
    {
        services.AddSingleton(x =>
            new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Url}:{settings.Port}/"));
        services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(settings.Database));

        services.AddTransient<BaseMongoRepository<ReviewEntityInfo>, MongoDbRepository<ReviewEntityInfo>>();
        services.AddTransient<IProjectionRepository<ReviewEntityInfo>, MongoDbRepository<ReviewEntityInfo>>();

        return services;
    }
}