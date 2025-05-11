using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Result>
{
    private readonly IRepository<DomainProduct> _productRepository;
    private readonly IMediator _mediator;
    private readonly IBus _bus;
    public UpdateProductCommandHandler(IRepository<DomainProduct> productRepository, IMediator mediator, IBus bus)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _bus = bus;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            Console.WriteLine("Product not found");

            product.UpdateProduct(
                request.ProductTitle,
                request.ProductDescription,
                request.ProductPrice,
                true,
                DateTime.Now,
                request.UserId,
                request.ApartmentTypeId,
                request.AddressLegalId
            );

        await _productRepository.UpdateAsync(product, cancellationToken);
        
        await _bus.Publish(new ProductSignalRUpdatedEvent
        {
            ProductId = product.Id,
            ProductTitle = product.Title,
            ProductDescription = product.Description,
            ProductPrice = product.Price,
        });

        await _mediator.Publish(new ProductUpdatedEvent(
            product.Id,
            product.Title,
            product.Description,
            product.Price,
            product.IsAvailable,
            product.CreatedAt,
            product.UserId,
            product.ApartmentTypeId,
            product.AddressLegalId
        ), cancellationToken);
        return Result.Success();
    }
}