using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;

public class LanguageEntityInfo : IQueryEntity
{
    public string Name { get; set; } = default!;
}