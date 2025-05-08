using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.QueryObjects;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using MongoDB.Driver;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Queries.GetProductTagsByProductIdQuery;

public class GetProductTagsPaginatedQueryHandler : IQueryHandler<GetProductTagsPaginatedQuery, Result<IEnumerable<ProductTagEntityInfo>>>
{
    private readonly BaseMongoRepository<ProductTagEntityInfo> _repository;
    private readonly BaseMongoRepository<TagEntityInfo> _Tagrepository;

    public GetProductTagsPaginatedQueryHandler(BaseMongoRepository<ProductTagEntityInfo> repository, BaseMongoRepository<TagEntityInfo> tagrepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _Tagrepository = tagrepository;
    }

    public async Task<Result<IEnumerable<ProductTagEntityInfo>>> Handle(
        GetProductTagsPaginatedQuery request, 
        CancellationToken cancellationToken)
    {
        var builder = Builders<ProductTagEntityInfo>.Filter;
        var filters = new List<FilterDefinition<ProductTagEntityInfo>>();

        if (request.ProductId > 0)
            filters.Add(builder.Eq(pt => pt.ProductId, request.ProductId));

        if (request.Name != null && request.Name.Any())
        {
            var tagFilter = Builders<TagEntityInfo>.Filter.Or(
                request.Name.Select(name =>
                    Builders<TagEntityInfo>.Filter.Regex(t => t.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"))
                )
            );

            var matchedTags = await _Tagrepository.FindWithFilterAsync(tagFilter);
            var matchedTagIds = matchedTags.Select(t => t.Id).ToList();

            filters.Add(builder.In(pt => pt.TagId, matchedTagIds));
        }

        var finalFilter = filters.Any() ? builder.And(filters) : builder.Empty;

        var sort = request.SortOrder switch
        {
            TagSortState.NameAsc => Builders<ProductTagEntityInfo>.Sort.Ascending(t => t.TagName),
            TagSortState.NameDesc => Builders<ProductTagEntityInfo>.Sort.Descending(t => t.TagName),
            _ => Builders<ProductTagEntityInfo>.Sort.Ascending(t => t.TagName)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: finalFilter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        var tags = result.Items;
        var tagIds = tags.Select(t => t.TagId).Distinct().ToList();
        var tagDocs = await _Tagrepository.FindByIdsAsync(tagIds);
        var tagMap = tagDocs.ToDictionary(t => t.Id, t => t.Name);

        foreach (var tag in tags)
            tag.TagName = tagMap.TryGetValue(tag.TagId, out var name) ? name : "[Deleted]";

        return Result<IEnumerable<ProductTagEntityInfo>>.Success(tags);
    }
}


public static class ProductTagFilterBuilder
{
    public static FilterDefinition<ProductTagEntityInfo> Build(GetProductTagsPaginatedQuery request)
    {
        var builder = Builders<ProductTagEntityInfo>.Filter;
        var filters = new List<FilterDefinition<ProductTagEntityInfo>>();

        if (request.ProductId > 0)
            filters.Add(builder.Eq(pt => pt.ProductId, request.ProductId));

        if (request.Name != null && request.Name.Any())
        {
            var nameFilters = request.Name.Select(name =>
                builder.Regex(pt => pt.TagName, new MongoDB.Bson.BsonRegularExpression(name, "i"))
            );
            filters.Add(builder.Or(nameFilters));
        }

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}