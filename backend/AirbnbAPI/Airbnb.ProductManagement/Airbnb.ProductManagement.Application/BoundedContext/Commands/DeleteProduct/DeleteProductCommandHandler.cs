using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Result>
{
    private readonly IRepository<DomainProduct> _productRepository;
    private readonly IMediator _mediator;
    private readonly IBus _bus;

    public DeleteProductCommandHandler(IRepository<DomainProduct> productRepository, IMediator mediator, IBus bus)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _bus = bus;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            // return Result.Failure("Продукт не найден");

        await _productRepository.DeleteAsync(request.Id, cancellationToken);

        // Публикуем событие об удалении
        await _mediator.Publish(new ProductDeletedEvent(product.Id), cancellationToken);
        
        await _bus.Publish(new ProductReviewDeletedEvent
        {
            Id = product.Id,
        }, cancellationToken);

        return Result.Success();
    }
}