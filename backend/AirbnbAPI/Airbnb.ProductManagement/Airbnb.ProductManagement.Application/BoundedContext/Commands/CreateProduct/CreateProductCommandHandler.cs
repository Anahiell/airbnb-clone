using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IRepository<DomainProduct> productRepository,
    IRepository<AddressLegal> addressRepository,
    IRepository<ApartmentType> apartmentTypeRepository,
    IMediator mediator)
    : ICommandHandler<CreateProductCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var address = new AddressLegal();
        address.CreateAddress(
            request.Region,
            request.Country,
            request.City,
            request.District,
            request.House,
            request.Block,
            request.Flat
        );
        
        await addressRepository.AddAsync(address, cancellationToken);

        var product = new DomainProduct(
            productTitle: request.ProductTitle,
            productDescription: request.ProductDescription,
            productPrice: request.ProductPrice,
            orderDate: DateTime.UtcNow,
            userId: request.UserId,
            apartmentTypeId: (int)request.ApartmentType,
            addressLegalId: address.Id
        );
        
        var apartmentType = await apartmentTypeRepository.GetByIdAsync((int)request.ApartmentType, cancellationToken);
        
        if (apartmentType == null)
        {
            // return Result<int>.Failure("Invalid ApartmentTypeId.");
        }

        var result = await productRepository.AddAsync(product, cancellationToken);

        await mediator.Publish(new ProductCreatedEvent(product.Id, request.ProductTitle, request.ProductDescription,
            request.ProductPrice, true, DateTime.UtcNow, request.UserId, apartmentType.Id,
            address.Id), cancellationToken);

        return Result<int>.Success(result);
    }
}
