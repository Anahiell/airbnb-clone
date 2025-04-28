using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries;

public class GetUserPaginatedQueryHandler : IQueryHandler<GetUserPaginatedQuery, Result<IEnumerable<UserEntityInfo>>>
{
    private readonly BaseMongoRepository<UserEntityInfo> _repository;

    public GetUserPaginatedQueryHandler(BaseMongoRepository<UserEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<UserEntityInfo>>> Handle(GetUserPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = UserFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            UserSortState.CreatedAtAsc => Builders<UserEntityInfo>.Sort.Ascending(r => r.CreatedAt),
            UserSortState.CreatedAtDesc => Builders<UserEntityInfo>.Sort.Descending(r => r.CreatedAt),
            _ => Builders<UserEntityInfo>.Sort.Ascending(r => r.Id)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        Console.WriteLine($"Users count: {result.Items.Count()}, TotalCount: {result.TotalCount}");
        return Result<IEnumerable<UserEntityInfo>>.Success(result.Items);
    }
}