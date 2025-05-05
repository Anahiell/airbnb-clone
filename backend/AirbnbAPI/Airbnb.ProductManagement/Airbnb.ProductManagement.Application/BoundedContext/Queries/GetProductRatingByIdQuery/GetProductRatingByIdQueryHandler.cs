using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries.GetProductRatingByIdQuery;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Result<ProductEntityInfo>>
{
    private readonly BaseMongoRepository<ProductEntityInfo> _repository;
    private readonly IProductDataAggregator _aggregator;

    public GetProductByIdQueryHandler(BaseMongoRepository<ProductEntityInfo> repository,
        IProductDataAggregator httpConnectionService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _aggregator = httpConnectionService ?? throw new ArgumentNullException(nameof(httpConnectionService));
    }

    public async Task<Result<ProductEntityInfo>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.FindByIdAsync(request.ProductId);

        if (product == null)
        {
            // return Result<ProductEntityInfo>.Failure("Product not found");
        }

        await _aggregator.EnrichAsync(new List<ProductEntityInfo> { product }, cancellationToken);

        return Result<ProductEntityInfo>.Success(product);
    }
}