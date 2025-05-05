using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.PictureManagement.Application.BoundedContext.Queries;

public class GetProductPicturePaginatedQueryHandler : IQueryHandler<GetProductPicturesPaginatedQuery, Result<IEnumerable<PictureEntityInfo>>>
{
    private readonly BaseMongoRepository<PictureEntityInfo> _repository;

    public GetProductPicturePaginatedQueryHandler(BaseMongoRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<PictureEntityInfo>>> Handle(GetProductPicturesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = PictureFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            PictureSortState.CreatedAtAsc => Builders<PictureEntityInfo>.Sort.Ascending(p => p.CreatedAt),
            PictureSortState.CreatedAtDesc => Builders<PictureEntityInfo>.Sort.Descending(p => p.CreatedAt),
            _ => Builders<PictureEntityInfo>.Sort.Descending(p => p.CreatedAt)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        Console.WriteLine($"Pictures count: {result.Items.Count()}, TotalCount: {result.TotalCount}");

        return Result<IEnumerable<PictureEntityInfo>>.Success(result.Items);
    }
}

public static class PictureFilterBuilder
{
    public static FilterDefinition<PictureEntityInfo> Build(GetProductPicturesPaginatedQuery request)
    {
        var builder = Builders<PictureEntityInfo>.Filter;
        var filters = new List<FilterDefinition<PictureEntityInfo>>();

        if (request.CreatedAfter.HasValue)
            filters.Add(builder.Gte(p => p.CreatedAt, request.CreatedAfter.Value));

        if (request.CreatedBefore.HasValue)
            filters.Add(builder.Lte(p => p.CreatedAt, request.CreatedBefore.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}