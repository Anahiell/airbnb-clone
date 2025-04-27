using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

public class CreatePictureCommandHandler(IRepository<DomainPicture> pictureRepository, IMediator mediator)
    : ICommandHandler<CreatePictureCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreatePictureCommand request, CancellationToken cancellationToken)
    {
        var picture = new DomainPicture(request.Url, request.UserId, DateTime.UtcNow);

        var result = await pictureRepository.AddAsync(picture, cancellationToken);

        await mediator.Publish(new PictureCreatedEvent(picture.Id, picture.Url, picture.UserId, picture.CreatedAt), cancellationToken);

        return Result<int>.Success(result);
    }
}