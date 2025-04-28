using Airbnb.MongoRepository.Configuration;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.TagManagement.API.Extensions;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDbService(this IServiceCollection services, MongoDbSettings settings)
    {
        services.AddSingleton(x =>
            new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Url}:{settings.Port}/"));
        services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(settings.Database));

        services.AddTransient<BaseMongoRepository<UserEntityInfo>, MongoDbRepository<UserEntityInfo>>();
        services.AddTransient<IProjectionRepository<UserEntityInfo>, MongoDbRepository<UserEntityInfo>>();

        return services;
    }
}