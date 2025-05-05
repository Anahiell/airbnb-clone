using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPicturesPaginatedQuery;

public class GetUserPicturesPaginatedQueryHandler : IQueryHandler<GetUserPicturesPaginatedQuery, Result<IEnumerable<UserPictureEntityInfo>>>
{
    private readonly BaseMongoRepository<UserPictureEntityInfo > _repository;

    public GetUserPicturesPaginatedQueryHandler(BaseMongoRepository<UserPictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<UserPictureEntityInfo>>> Handle(GetUserPicturesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = BuildFilter(request);

        var sort = request.SortOrder switch
        {
            PictureSortState.CreatedAtAsc => Builders<UserPictureEntityInfo>.Sort.Ascending(p => p.CreatedAt),
            PictureSortState.CreatedAtDesc => Builders<UserPictureEntityInfo>.Sort.Descending(p => p.CreatedAt),
            _ => Builders<UserPictureEntityInfo>.Sort.Descending(p => p.CreatedAt)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        return Result<IEnumerable<UserPictureEntityInfo>>.Success(result.Items);
    }

    private static FilterDefinition<UserPictureEntityInfo> BuildFilter(GetUserPicturesPaginatedQuery request)
    {
        var builder = Builders<UserPictureEntityInfo>.Filter;
        var filters = new List<FilterDefinition<UserPictureEntityInfo>>
        {
            builder.Eq(p => p.UserId, request.UserId)
        };

        if (request.CreatedAfter.HasValue)
            filters.Add(builder.Gte(p => p.CreatedAt, request.CreatedAfter.Value));

        if (request.CreatedBefore.HasValue)
            filters.Add(builder.Lte(p => p.CreatedAt, request.CreatedBefore.Value));

        return builder.And(filters);
    }
}