using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public class CreateProductCommandHandler(IRepository<DomainProduct> productRepository,
    IMediator mediator)
    : ICommandHandler<CreateProductCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new DomainProduct(request.ProductTitle, request.ProductDescription, request.ProductPrice,
            DateTime.UtcNow, request.UserId, request.ApartmentTypeId, request.AddressLegalId
        );

        var result = await productRepository.AddAsync(product, cancellationToken);

        await mediator.Publish(new ProductCreatedEvent(product.Id, request.ProductTitle, request.ProductDescription,
            request.ProductPrice, true, DateTime.UtcNow, request.UserId, request.ApartmentTypeId,
            request.AddressLegalId), cancellationToken);

        return Result<int>.Success(result);
    }
}