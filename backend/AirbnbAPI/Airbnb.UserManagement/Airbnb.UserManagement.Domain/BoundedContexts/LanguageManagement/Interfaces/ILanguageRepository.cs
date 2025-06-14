using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;

public interface ILanguageRepository : IRepository<DomainLanguage>
{
    Task<DomainLanguage?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainLanguage>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
}