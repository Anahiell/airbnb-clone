using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByIdQuery;

public class GetUserByIdQuery : IQuery<Result<UserEntityInfo>>
{
    public int Id { get; set; }
}