using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByProductIdQuery;

public class GetUserByProductIdQuery : IQuery<Result<UserEntityInfo>>
{
    public int ProductId { get; set; }
}