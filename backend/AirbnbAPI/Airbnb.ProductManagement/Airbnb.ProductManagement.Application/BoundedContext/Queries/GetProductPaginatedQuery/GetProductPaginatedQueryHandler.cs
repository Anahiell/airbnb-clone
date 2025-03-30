using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;
using MongoDB.Driver;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQueryHandler : IQueryHandler<GetProductPaginatedQuery,
    Result<IEnumerable<ProductEntityInfo>>>
{
    private readonly BaseMongoRepository<ProductEntityInfo> _repository;

    public GetProductPaginatedQueryHandler(BaseMongoRepository<ProductEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<ProductEntityInfo>>> Handle(
        GetProductPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var filter = ProductFilterBuilder.Build(request);
        
        var sort = request.SortOrder switch
        {
            SortState.PriceAsc => Builders<ProductEntityInfo>.Sort.Ascending(p => p.Price),
            SortState.PriceDesc => Builders<ProductEntityInfo>.Sort.Descending(p => p.Price),
            _ => Builders<ProductEntityInfo>.Sort.Descending(p => p.CreatedDate)
        };
        
        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );
        Console.WriteLine($"Items count: {result.Items.Count()}, TotalCount: {result.TotalCount}");
        return Result<IEnumerable<ProductEntityInfo>>.Success(result.Items);
    }
}

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