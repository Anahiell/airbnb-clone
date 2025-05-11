using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IRepository<DomainProduct> productRepository,
    IRepository<AddressLegal> addressRepository,
    IRepository<ApartmentType> apartmentTypeRepository,
    IBus bus,
    ILogger<CreateProductCommandHandler> _logger,
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
        
        // MassTransit
        await bus.Publish(new ProductTagUpdatedEvent
        {
            ProductId = product.Id,
            ProductTags = request.ProductTags,
        }, cancellationToken);

        await bus.Publish(new ProductSignalRCreatedEvent
        {
            ProductId = product.Id,
            ProductTitle = product.Title,
            ProductDescription = product.Description,
            ProductPrice = product.Price,
        });

        foreach (var file in request.PictureFiles)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            var pictureData = memoryStream.ToArray();

            await bus.Publish(new ProductPictureUpdatedEvent
            {
                ProductId = product.Id,
                PictureData = pictureData
            }, cancellationToken);
        }

        return Result<int>.Success(result);
    }
}
