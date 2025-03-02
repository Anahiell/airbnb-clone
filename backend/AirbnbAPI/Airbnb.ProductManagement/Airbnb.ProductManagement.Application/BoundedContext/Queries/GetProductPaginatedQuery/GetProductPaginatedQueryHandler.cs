using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;
using MongoDB.Driver;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQueryHandler : IRequestHandler<GetProductPaginatedQuery, (IEnumerable<ProductEntityInfo> Items, long TotalCount)>
{
    // TODO Сегрегация запросов на нереляционную БД
    private readonly BaseMongoRepository<ProductEntityInfo> _repository;

    public GetProductPaginatedQueryHandler(BaseMongoRepository<ProductEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<(IEnumerable<ProductEntityInfo> Items, long TotalCount)> Handle(GetProductPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetFilteredPaginatedAsync(
            filter: p => p.Price > 100,
            sort: Builders<ProductEntityInfo>.Sort.Descending(p => p.Price),
            page: 2,
            pageSize: 10
        );
    }
}