using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Result>
{
    private readonly IRepository<DomainProduct> _productRepository;
    private readonly IMediator _mediator;

    public DeleteProductCommandHandler(IRepository<DomainProduct> productRepository, IMediator mediator)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            // return Result.Failure("Продукт не найден");

        await _productRepository.DeleteAsync(request.Id, cancellationToken);

        // Публикуем событие об удалении
        await _mediator.Publish(new ProductDeletedEvent(product.Id), cancellationToken);

        return Result.Success();
    }
}