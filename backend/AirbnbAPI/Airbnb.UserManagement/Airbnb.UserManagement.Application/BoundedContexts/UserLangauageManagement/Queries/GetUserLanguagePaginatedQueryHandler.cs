using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Queries;

public class GetUserLanguagePaginatedQueryHandler : IQueryHandler<GetUserLanguagePaginatedQuery, Result<IEnumerable<UserLanguageEntityInfo>>>
{
    private readonly BaseMongoRepository<UserLanguageEntityInfo> _repository;

    public GetUserLanguagePaginatedQueryHandler(BaseMongoRepository<UserLanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<IEnumerable<UserLanguageEntityInfo>>> Handle(
        GetUserLanguagePaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var filter = UserLanguageFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            UserLanguageSortState.LanguageNameAsc => Builders<UserLanguageEntityInfo>.Sort.Ascending(x => x.LanguageName),
            UserLanguageSortState.LanguageNameDesc => Builders<UserLanguageEntityInfo>.Sort.Descending(x => x.LanguageName),
            _ => Builders<UserLanguageEntityInfo>.Sort.Ascending(x => x.Id)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        return Result<IEnumerable<UserLanguageEntityInfo>>.Success(result.Items);
    }
}