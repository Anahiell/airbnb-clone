using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;

public interface IUserRoleRepository
{
    Task<DomainUserRole?> GetByUserIdAndRoleIdAsync(int userId, int roleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainUserRole>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainUserRole>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken = default);

    Task<int> AddAsync(DomainUserRole userRole, CancellationToken cancellationToken = default);
    Task RemoveAsync(int userId, int roleId, CancellationToken cancellationToken = default);
    Task UpdateAsync(DomainUserRole entity, CancellationToken cancellationToken = default);
    Task DeleteAllByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}