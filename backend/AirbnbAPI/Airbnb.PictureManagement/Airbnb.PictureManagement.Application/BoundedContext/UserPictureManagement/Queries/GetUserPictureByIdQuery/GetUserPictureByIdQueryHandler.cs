using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;
using MongoDB.Driver;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPictureByIdQuery;

public class GetUserPictureByIdQueryHandler : IQueryHandler<GetUserPictureByIdQuery, Result<UserPictureEntityInfo>>
{
    private readonly BaseMongoRepository<UserPictureEntityInfo> _repository;

    public GetUserPictureByIdQueryHandler(BaseMongoRepository<UserPictureEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserPictureEntityInfo>> Handle(GetUserPictureByIdQuery request, CancellationToken cancellationToken)
    {
        var userPicture = await _repository.FindByAsync(p => p.UserId == request.UserId);

        if (userPicture == null)
        {
            // return Result<UserPictureEntityInfo>.Failure("Картинка пользователя не найдена");   
        }

        return Result<UserPictureEntityInfo>.Success(userPicture.FirstOrDefault());
        
    }
}