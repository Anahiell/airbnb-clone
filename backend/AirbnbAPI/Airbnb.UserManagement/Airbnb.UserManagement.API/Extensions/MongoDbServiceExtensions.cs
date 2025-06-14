using Airbnb.MongoRepository.Configuration;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;
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
        
        services.AddTransient<BaseMongoRepository<LanguageEntityInfo>, MongoDbRepository<LanguageEntityInfo>>();
        services.AddTransient<IProjectionRepository<LanguageEntityInfo>, MongoDbRepository<LanguageEntityInfo>>();
        
        services.AddTransient<BaseMongoRepository<UserLanguageEntityInfo>, MongoDbRepository<UserLanguageEntityInfo>>();
        services.AddTransient<IProjectionRepository<UserLanguageEntityInfo>, MongoDbRepository<UserLanguageEntityInfo>>();
        
        services.AddTransient<BaseMongoRepository<RoleEntityInfo>, MongoDbRepository<RoleEntityInfo>>();
        services.AddTransient<IProjectionRepository<RoleEntityInfo>, MongoDbRepository<RoleEntityInfo>>();

        services.AddTransient<BaseMongoRepository<PermissionEntityInfo>, MongoDbRepository<PermissionEntityInfo>>();
        services.AddTransient<IProjectionRepository<PermissionEntityInfo>, MongoDbRepository<PermissionEntityInfo>>();

        services.AddTransient<BaseMongoRepository<UserRoleEntityInfo>, MongoDbRepository<UserRoleEntityInfo>>();
        services.AddTransient<IProjectionRepository<UserRoleEntityInfo>, MongoDbRepository<UserRoleEntityInfo>>();

        services.AddTransient<BaseMongoRepository<UserPermissionEntityInfo>, MongoDbRepository<UserPermissionEntityInfo>>();
        services.AddTransient<IProjectionRepository<UserPermissionEntityInfo>, MongoDbRepository<UserPermissionEntityInfo>>();


        return services;
    }
}