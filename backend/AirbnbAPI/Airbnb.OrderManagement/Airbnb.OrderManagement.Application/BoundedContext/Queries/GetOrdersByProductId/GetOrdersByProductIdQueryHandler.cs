using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetOrdersByProductId;

public class GetOrdersByProductIdQueryHandler : IQueryHandler<GetOrdersByProductIdQuery, Result<IEnumerable<OrderEntityInfo>>>
{
    private readonly BaseMongoRepository<OrderEntityInfo> _repository;

    public GetOrdersByProductIdQueryHandler(BaseMongoRepository<OrderEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<OrderEntityInfo>>> Handle(GetOrdersByProductIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.FindByAsync(o => o.ProductId == request.ProductId);

        return Result<IEnumerable<OrderEntityInfo>>.Success(orders);
    }
}