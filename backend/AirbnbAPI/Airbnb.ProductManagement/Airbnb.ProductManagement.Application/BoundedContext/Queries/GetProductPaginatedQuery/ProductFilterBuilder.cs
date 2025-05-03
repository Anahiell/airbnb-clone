using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public static class ProductFilterBuilder
{
    public static FilterDefinition<ProductEntityInfo> Build(GetProductPaginatedQuery request)
    {
        var builder = Builders<ProductEntityInfo>.Filter;
        var filters = new List<FilterDefinition<ProductEntityInfo>>();

        if (request.MinPrice.HasValue)
            filters.Add(builder.Gte(p => p.Price, request.MinPrice.Value));

        if (request.MaxPrice.HasValue)
            filters.Add(builder.Lte(p => p.Price, request.MaxPrice.Value));

        if (request.DateStart.HasValue)
            filters.Add(builder.Gte(p => p.CreatedDate, request.DateStart.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}