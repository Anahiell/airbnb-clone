using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Queries.GetProductPictureByIdQuery;

public class GetProductPictureByIdQueryHandler : IQueryHandler<GetProductPictureByIdQuery, Result<PictureEntityInfo>>
{
    private readonly BaseMongoRepository<PictureEntityInfo> _repository;

    public GetProductPictureByIdQueryHandler(BaseMongoRepository<PictureEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PictureEntityInfo>> Handle(GetProductPictureByIdQuery request, CancellationToken cancellationToken)
    {
        var picture = await _repository.FindByAsync(p => p.ProductId == request.ProductId);

        if (picture == null)
        {
            // return Result<PictureEntityInfo>.Failure("Картинка продукта не найдена");
        }

        return Result<PictureEntityInfo>.Success(picture.FirstOrDefault());
    }
}