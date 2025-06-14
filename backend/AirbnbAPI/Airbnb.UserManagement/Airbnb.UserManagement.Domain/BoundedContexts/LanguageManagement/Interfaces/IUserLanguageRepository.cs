using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;

public interface IUserLanguageRepository
{
    Task<DomainUserLanguage?> GetByUserIdAndLanguageIdAsync(int userId, int languageId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainUserLanguage>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainUserLanguage>> GetByLanguageIdAsync(int languageId, CancellationToken cancellationToken = default);

    Task<int> AddAsync(DomainUserLanguage userLanguage, CancellationToken cancellationToken = default);
    Task RemoveAsync(int userId, int languageId, CancellationToken cancellationToken = default);
    
    Task DeleteAllByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task UpdateAsync(DomainUserLanguage entity, CancellationToken cancellationToken = default);
}