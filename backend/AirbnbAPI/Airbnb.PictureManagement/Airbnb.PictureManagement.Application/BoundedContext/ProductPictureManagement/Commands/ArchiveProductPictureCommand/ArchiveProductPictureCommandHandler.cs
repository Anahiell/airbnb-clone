using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Commands.ArchiveProductPictureCommand;

public class ArchiveProductPictureCommandHandler : ICommandHandler<ArchiveProductPictureCommand, Result>
{
    private readonly IRepository<ProductPicture> _productPictureRepository;
    private readonly IMediator _mediator;

    public ArchiveProductPictureCommandHandler(IRepository<ProductPicture> productPictureRepository, IMediator mediator)
    {
        _productPictureRepository = productPictureRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(ArchiveProductPictureCommand request, CancellationToken cancellationToken)
    {
        // Проверяем, существует ли картинка с данным Id
        var productPicture = await _productPictureRepository.GetByIdAsync(request.Id, cancellationToken);

        if (productPicture == null)
        {
            // return Result.Failure("Картинка продукта не найдена");
        }

        // Архивируем картинку, не удаляя её физически
        productPicture.Archive();

        // Сохраняем изменения
        await _productPictureRepository.UpdateAsync(productPicture, cancellationToken);

        // Публикуем событие архивирования
        await _mediator.Publish(new ProductPictureArchivedEvent(productPicture.Id), cancellationToken);

        return Result.Success();
    }
}
