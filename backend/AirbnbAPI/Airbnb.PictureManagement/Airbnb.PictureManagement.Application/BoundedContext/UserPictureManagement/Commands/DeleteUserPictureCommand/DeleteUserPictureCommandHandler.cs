using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.DeleteUserPictureCommand;

public class DeleteUserPictureCommandHandler : ICommandHandler<DeleteUserPictureCommand, Result>
{
    private readonly IRepository<UserPicture> _userPictureRepository;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;

    public DeleteUserPictureCommandHandler(IRepository<UserPicture> userPictureRepository, IMediator mediator, IFileService fileService)
    {
        _userPictureRepository = userPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task<Result> Handle(DeleteUserPictureCommand request, CancellationToken cancellationToken)
    {
        var userPicture = await _userPictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (userPicture is null)
        {
            // return Result.Failure("Картинка не найдена");
        }

        await _fileService.DeleteAsync(userPicture.Url, "User");

        await _userPictureRepository.DeleteAsync(request.Id, cancellationToken);

        await _mediator.Publish(new UserPictureDeletedEvent(userPicture.Id), cancellationToken);

        return Result.Success();
    }
}