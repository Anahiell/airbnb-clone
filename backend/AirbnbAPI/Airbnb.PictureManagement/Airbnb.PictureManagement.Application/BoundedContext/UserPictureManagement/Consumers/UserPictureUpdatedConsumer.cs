using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UserPictureUpdatedEvent = Airbnb.ProductManagement.Application.BoundedContext.Events.UserPictureUpdatedEvent;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Consumers;


public class UserPictureUpdatedConsumer : IConsumer<UserPictureUpdatedEvent>
{
    private readonly IRepository<UserPicture> _userPictureRepository;
    private readonly ILogger<UserPictureUpdatedConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;

    public UserPictureUpdatedConsumer(
        IRepository<UserPicture> userPictureRepository,
        ILogger<UserPictureUpdatedConsumer> logger,
        IFileService fileService,
        IMediator mediator)
    {
        _userPictureRepository = userPictureRepository;
        _logger = logger;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task Consume(ConsumeContext<UserPictureUpdatedEvent> context)
    {
        var eventMessage = context.Message;
        _logger.LogInformation("Start processing UserPictureUpdatedEvent for Url: {UserId}", eventMessage.UserId);

        try
        {
            var stream = new MemoryStream(eventMessage.PictureData);
            var formFile = new FormFile(stream, 0, eventMessage.PictureData.Length, "picture", $"{Guid.NewGuid()}.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var relativeUrl = await _fileService.SaveAsync(formFile, "User", context.CancellationToken);

            var picture = new UserPicture(Guid.NewGuid(), relativeUrl, eventMessage.UserId, DateTime.UtcNow);
            await _userPictureRepository.AddAsync(picture, context.CancellationToken);

            await _mediator.Publish(new UserPictureCreatedEvent(
                picture.Id, relativeUrl, picture.UserId, picture.CreatedAt), context.CancellationToken);

            _logger.LogInformation("Successfully processed and saved picture for UserId: {UserId}", eventMessage.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process UserPictureUpdatedEvent for UserId: {UserId}", eventMessage.UserId);
            throw;
        }

        _logger.LogInformation("Finished processing UserPictureUpdatedEvent.");
    }
}