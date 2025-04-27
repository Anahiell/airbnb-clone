using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetProductPaginatedQuery;

public class GetOrderPaginatedQueryHandler : IQueryHandler<GetOrderPaginatedQuery, Result<IEnumerable<OrderEntityInfo>>>
{
    private readonly BaseMongoRepository<OrderEntityInfo> _repository;

    public GetOrderPaginatedQueryHandler(BaseMongoRepository<OrderEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<OrderEntityInfo>>> Handle(GetOrderPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = OrderFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            OrderSortState.DateStartAsc => Builders<OrderEntityInfo>.Sort.Ascending(o => o.DateStart),
            OrderSortState.DateStartDesc => Builders<OrderEntityInfo>.Sort.Descending(o => o.DateStart),
            _ => Builders<OrderEntityInfo>.Sort.Descending(o => o.DateStart)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        Console.WriteLine($"Orders count: {result.Items.Count()}, TotalCount: {result.TotalCount}");
        return Result<IEnumerable<OrderEntityInfo>>.Success(result.Items);
    }
}