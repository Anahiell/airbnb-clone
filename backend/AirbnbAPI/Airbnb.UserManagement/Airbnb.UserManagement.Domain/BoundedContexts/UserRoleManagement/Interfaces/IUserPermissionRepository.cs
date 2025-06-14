using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;

public interface IUserPermissionRepository
{
    Task<DomainUserPermission?> GetByUserIdAndPermissionIdAsync(int userId, int permissionId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainUserPermission>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainUserPermission>> GetByPermissionIdAsync(int permissionId, CancellationToken cancellationToken = default);
    Task<int> AddAsync(DomainUserPermission userPermission, CancellationToken cancellationToken = default);
    Task RemoveAsync(int userId, int permissionId, CancellationToken cancellationToken = default);
    Task UpdateAsync(DomainUserPermission userPermission, CancellationToken cancellationToken = default);
    
    Task DeleteAllByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}