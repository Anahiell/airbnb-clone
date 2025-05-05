using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Queries.GetProductRatingByIdQuery;

public class GetProductRatingByIdQueryHandler : IQueryHandler<GetProductRatingByIdQuery, Result<decimal>>
{
    private readonly BaseMongoRepository<ReviewEntityInfo> _repository;

    public GetProductRatingByIdQueryHandler(BaseMongoRepository<ReviewEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<decimal>> Handle(GetProductRatingByIdQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.FindByAsync(r => r.ProductId == request.ProductId);

        var reviewEntityInfos = reviews as ReviewEntityInfo[] ?? reviews.ToArray();
        if (!reviewEntityInfos.Any())
            return Result<decimal>.Success(0);

        var averageRating = (decimal)Math.Round(reviewEntityInfos.Average(r => r.Rating), 1);

        return Result<decimal>.Success(averageRating);
    }
}