using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;

public interface IUserRepository : IRepository<DomainUser>
{
    Task<DomainUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IList<UserRole>> GetRolesAsync(DomainUser user);
}