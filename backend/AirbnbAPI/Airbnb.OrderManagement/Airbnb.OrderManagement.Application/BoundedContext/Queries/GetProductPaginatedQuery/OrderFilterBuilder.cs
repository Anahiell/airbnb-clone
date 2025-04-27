using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetProductPaginatedQuery;

public static class OrderFilterBuilder
{
    public static FilterDefinition<OrderEntityInfo> Build(GetOrderPaginatedQuery request)
    {
        var builder = Builders<OrderEntityInfo>.Filter;
        var filters = new List<FilterDefinition<OrderEntityInfo>>();

        if (request.ProductId.HasValue)
            filters.Add(builder.Eq(o => o.ProductId, request.ProductId.Value));

        if (request.UserId.HasValue)
            filters.Add(builder.Eq(o => o.UserId, request.UserId.Value));

        if (request.DateStartAfter.HasValue)
            filters.Add(builder.Gte(o => o.DateStart, request.DateStartAfter.Value));

        if (request.DateEndBefore.HasValue)
            filters.Add(builder.Lte(o => o.DateEnd, request.DateEndBefore.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}