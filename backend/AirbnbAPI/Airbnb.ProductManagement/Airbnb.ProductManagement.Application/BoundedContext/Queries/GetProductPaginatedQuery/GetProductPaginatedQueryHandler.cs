using System.Text.Json;
using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Connection.ConnectionRealization;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using MediatR;
using MongoDB.Driver;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQueryHandler : IQueryHandler<GetProductPaginatedQuery,
    Result<IEnumerable<ProductEntityInfo>>>
{
    private readonly BaseMongoRepository<ProductEntityInfo> _repository;
    private readonly IProductDataAggregator _aggregator;

    public GetProductPaginatedQueryHandler(BaseMongoRepository<ProductEntityInfo> repository,
        IProductDataAggregator httpConnectionService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _aggregator = httpConnectionService ?? throw new ArgumentNullException(nameof(httpConnectionService));
    }

    public async Task<Result<IEnumerable<ProductEntityInfo>>> Handle(
        GetProductPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = ProductFilterBuilder.Build(request);
        var sort = request.SortOrder switch
        {
            SortState.PriceAsc => Builders<ProductEntityInfo>.Sort.Ascending(p => p.Price),
            SortState.PriceDesc => Builders<ProductEntityInfo>.Sort.Descending(p => p.Price),
            _ => Builders<ProductEntityInfo>.Sort.Descending(p => p.CreatedDate)
        };

        var result = await _repository.GetFilteredPaginatedAsync(filter, sort, request.Page, request.PageSize);
        var products = result.Items.ToList();

        await _aggregator.EnrichAsync(products, cancellationToken);

        return Result<IEnumerable<ProductEntityInfo>>.Success(products);
    }
}