using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Services;

public interface ITokenService
{
    string GenerateJwt(DomainUser user, IEnumerable<DomainUserRole> roles);
}