using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Queries;

public class GetAllLanguagesQueryHandler : IQueryHandler<GetAllLanguagesQuery, Result<IEnumerable<LanguageEntityInfo>>>
{
    private readonly BaseMongoRepository<LanguageEntityInfo> _repository;

    public GetAllLanguagesQueryHandler(BaseMongoRepository<LanguageEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<LanguageEntityInfo>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        var allLanguages = await _repository.GetAllAsync();

        return Result<IEnumerable<LanguageEntityInfo>>.Success(allLanguages);
    }
}