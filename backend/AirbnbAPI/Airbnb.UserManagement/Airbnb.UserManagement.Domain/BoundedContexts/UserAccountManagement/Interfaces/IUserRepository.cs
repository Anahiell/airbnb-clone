using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;

public interface IUserRepository
{
    Task<DomainUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}