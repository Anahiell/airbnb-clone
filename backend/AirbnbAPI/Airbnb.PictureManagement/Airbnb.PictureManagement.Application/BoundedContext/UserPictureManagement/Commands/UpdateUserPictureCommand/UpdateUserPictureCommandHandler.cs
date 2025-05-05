using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UpdateUserPictureCommand;

public class UpdateUserPictureCommandHandler : ICommandHandler<UpdateUserPictureCommand, Result>
{
    private readonly IFileService _fileService;
    private readonly IRepository<UserPicture> _userPictureRepository;
    private readonly IMediator _mediator;

    public UpdateUserPictureCommandHandler(IRepository<UserPicture> userPictureRepository,
        IMediator mediator, IFileService fileService)
    {
        _userPictureRepository = userPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task<Result> Handle(UpdateUserPictureCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.Id <= 0)
        {
            // return Result.Failure("Некорректный запрос");
        }

        var userPicture = await _userPictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (userPicture == null)
        {
            // return Result.Failure("Картинка не найдена");
        }

        await _fileService.DeleteAsync(userPicture.Url, "User");
        
        var newUrl = await _fileService.SaveAsync(request.File, "User", cancellationToken);

        userPicture.UpdatePictureUrl(newUrl);

        await _userPictureRepository.UpdateAsync(userPicture, cancellationToken);
        await _mediator.Publish(new UserPictureDeletedEvent(userPicture.Id), cancellationToken);
        await _mediator.Publish(new UserPictureCreatedEvent(userPicture.Id, newUrl, userPicture.UserId, DateTime.UtcNow), cancellationToken);

        return Result.Success();
    }
}