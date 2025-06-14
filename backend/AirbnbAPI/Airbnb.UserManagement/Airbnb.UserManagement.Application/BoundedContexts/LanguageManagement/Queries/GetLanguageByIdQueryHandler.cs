using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Queries;

public class GetLanguageByIdQueryHandler : IQueryHandler<GetLanguageByIdQuery, Result<LanguageEntityInfo>>
{
    private readonly BaseMongoRepository<LanguageEntityInfo> _repository;

    public GetLanguageByIdQueryHandler(BaseMongoRepository<LanguageEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<LanguageEntityInfo>> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var language = await _repository.FindByIdAsync(request.Id);

        return language is null
            ? Result<LanguageEntityInfo>.Failure("Язык не найден")
            : Result<LanguageEntityInfo>.Success(language);
    }
}