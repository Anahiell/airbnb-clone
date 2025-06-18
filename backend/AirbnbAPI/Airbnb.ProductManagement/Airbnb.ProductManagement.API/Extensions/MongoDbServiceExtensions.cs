using Airbnb.MongoRepository.Configuration;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;
using FeatureEntityInfo = Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects.FeatureEntityInfo;

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
        
        services.AddTransient<BaseMongoRepository<FacilityEntityInfo>, MongoDbRepository<FacilityEntityInfo>>();
        services.AddTransient<IProjectionRepository<FacilityEntityInfo>, MongoDbRepository<FacilityEntityInfo>>();
        
        services.AddTransient<BaseMongoRepository<FeatureEntityInfo>, MongoDbRepository<FeatureEntityInfo>>();
        services.AddTransient<IProjectionRepository<FeatureEntityInfo>, MongoDbRepository<FeatureEntityInfo>>();


        return services;
    }
}