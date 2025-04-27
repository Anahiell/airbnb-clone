using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Queries;

public class GetReviewPaginatedQueryHandler : IQueryHandler<GetReviewPaginatedQuery, Result<IEnumerable<ReviewEntityInfo>>>
{
    private readonly BaseMongoRepository<ReviewEntityInfo> _repository;

    public GetReviewPaginatedQueryHandler(BaseMongoRepository<ReviewEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<ReviewEntityInfo>>> Handle(GetReviewPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = ReviewFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            ReviewSortState.RatingAsc => Builders<ReviewEntityInfo>.Sort.Ascending(r => r.Rating),
            ReviewSortState.RatingDesc => Builders<ReviewEntityInfo>.Sort.Descending(r => r.Rating),
            _ => Builders<ReviewEntityInfo>.Sort.Descending(r => r.CreatedAt)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        Console.WriteLine($"Reviews count: {result.Items.Count()}, TotalCount: {result.TotalCount}");
        return Result<IEnumerable<ReviewEntityInfo>>.Success(result.Items);
    }
}

public static class ReviewFilterBuilder
{
    public static FilterDefinition<ReviewEntityInfo> Build(GetReviewPaginatedQuery request)
    {
        var builder = Builders<ReviewEntityInfo>.Filter;
        var filters = new List<FilterDefinition<ReviewEntityInfo>>();

        if (request.MinRating.HasValue)
            filters.Add(builder.Gte(r => r.Rating, request.MinRating.Value));

        if (request.MaxRating.HasValue)
            filters.Add(builder.Lte(r => r.Rating, request.MaxRating.Value));

        if (request.UserId.HasValue)
            filters.Add(builder.Eq(r => r.UserId, request.UserId.Value));

        if (request.ProductId.HasValue)
            filters.Add(builder.Eq(r => r.ProductId, request.ProductId.Value));

        if (request.CreatedAfter.HasValue)
            filters.Add(builder.Gte(r => r.CreatedAt, request.CreatedAfter.Value));

        if (request.CreatedBefore.HasValue)
            filters.Add(builder.Lte(r => r.CreatedAt, request.CreatedBefore.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}
