using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries;

public static class UserFilterBuilder
{
    public static FilterDefinition<UserEntityInfo> Build(GetUserPaginatedQuery request)
    {
        var builder = Builders<UserEntityInfo>.Filter;
        var filters = new List<FilterDefinition<UserEntityInfo>>();

        if (request.Role.HasValue)
            filters.Add(builder.Eq(r => r.Role, request.Role.Value));

        if (request.CreatedAfter.HasValue)
            filters.Add(builder.Gte(r => r.CreatedAt, request.CreatedAfter.Value));

        if (request.CreatedBefore.HasValue)
            filters.Add(builder.Lte(r => r.CreatedAt, request.CreatedBefore.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}