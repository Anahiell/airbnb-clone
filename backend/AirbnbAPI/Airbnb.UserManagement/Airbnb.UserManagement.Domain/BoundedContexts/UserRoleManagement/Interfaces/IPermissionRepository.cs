using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;

public interface IPermissionRepository : IRepository<DomainPermission>
{
    Task<DomainPermission?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainPermission>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
}