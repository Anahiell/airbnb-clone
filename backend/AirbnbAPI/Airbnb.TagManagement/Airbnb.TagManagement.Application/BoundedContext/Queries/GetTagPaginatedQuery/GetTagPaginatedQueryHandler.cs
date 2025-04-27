using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.TagsManagement.Application.BoundedContext.Queries.GetTagPaginatedQuery;


public class GetTagPaginatedQueryHandler : IQueryHandler<GetTagPaginatedQuery, Result<IEnumerable<TagEntityInfo>>>
{
    private readonly BaseMongoRepository<TagEntityInfo> _repository;

    public GetTagPaginatedQueryHandler(BaseMongoRepository<TagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<TagEntityInfo>>> Handle(
        GetTagPaginatedQuery request, 
        CancellationToken cancellationToken)
    {
        var filter = TagFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            TagSortState.NameAsc => Builders<TagEntityInfo>.Sort.Ascending(t => t.Name),
            TagSortState.NameDesc => Builders<TagEntityInfo>.Sort.Descending(t => t.Name),
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        return Result<IEnumerable<TagEntityInfo>>.Success(result.Items);
    }
}

public static class TagFilterBuilder
{
    public static FilterDefinition<TagEntityInfo> Build(GetTagPaginatedQuery request)
    {
        var builder = Builders<TagEntityInfo>.Filter;
        var filters = new List<FilterDefinition<TagEntityInfo>>();

        if (!string.IsNullOrEmpty(request.Name))
            filters.Add(builder.Regex(t => t.Name, new MongoDB.Bson.BsonRegularExpression(request.Name, "i")));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}