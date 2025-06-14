using Airbnb.SharedKernel.Repositories;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;

public interface IRoleRepository : IRepository<DomainRole>
{
    Task<DomainRole?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainRole>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
}