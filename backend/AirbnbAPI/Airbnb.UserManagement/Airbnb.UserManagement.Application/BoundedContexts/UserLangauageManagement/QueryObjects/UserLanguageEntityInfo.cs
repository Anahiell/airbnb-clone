using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;

public class UserLanguageEntityInfo : IQueryEntity
{
    public int UserId { get; set; }
    public int LanguageId { get; set; }
    public string LanguageName { get; set; } = default!;
}