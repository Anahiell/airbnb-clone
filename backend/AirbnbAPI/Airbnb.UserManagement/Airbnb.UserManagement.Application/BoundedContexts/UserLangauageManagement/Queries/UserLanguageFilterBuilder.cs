using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Queries;

public static class UserLanguageFilterBuilder
{
    public static FilterDefinition<UserLanguageEntityInfo> Build(GetUserLanguagePaginatedQuery request)
    {
        var builder = Builders<UserLanguageEntityInfo>.Filter;
        var filters = new List<FilterDefinition<UserLanguageEntityInfo>>();

        if (request.UserId.HasValue)
            filters.Add(builder.Eq(x => x.UserId, request.UserId.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}