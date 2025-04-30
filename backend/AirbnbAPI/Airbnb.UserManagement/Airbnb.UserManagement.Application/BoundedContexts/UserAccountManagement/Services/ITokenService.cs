using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Services;

public interface ITokenService
{
    string GenerateJwt(DomainUser user, IList<UserRole> roles);
}