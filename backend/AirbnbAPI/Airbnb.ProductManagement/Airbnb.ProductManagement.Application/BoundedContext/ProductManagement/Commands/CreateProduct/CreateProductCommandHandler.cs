using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Additional;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Advantage;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Coordinates;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Facility;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Feature;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Room;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Rule;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.User;
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
    IMediator mediator,
    IUseCaseDispatcher useCaseDispatcher)
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
        
        // --- Координаты ---
        if (request.Latitude is not null && request.Longitude is not null)
        {
            await useCaseDispatcher.DispatchAsync(
                new UpdateProductCoordinatesUseCase(result, request.Latitude, request.Longitude),
                cancellationToken);
        }

        // --- Правила ---
        if (request.GuestRules is not null)
        {
            await useCaseDispatcher.DispatchAsync(new UpdateProductGuestRulesUseCase(result, request.GuestRules),
                cancellationToken);
        }

        // --- Дополнительная информация ---
        if (request.CancelPolicy is not null || request.HomeRules is not null || request.SafetyRules is not null)
        {
            await useCaseDispatcher.DispatchAsync(
                new UpdateProductAdditionalInfoUseCase(result, request.CancelPolicy, request.HomeRules,
                    request.SafetyRules),
                cancellationToken);
        }

        // --- Преимущества ---
        if (request.Advantages is not null)
        {
            await useCaseDispatcher.DispatchAsync(new UpdateProductAdvantagesUseCase(result, request.Advantages),
                cancellationToken);
        }

        // --- Фичи ---
        if (request.Features is not null)
        {
            await useCaseDispatcher.DispatchAsync(new UpdateProductFeaturesUseCase(result, request.Features),
                cancellationToken);
        }

        // --- Удобства ---
        if (request.Facilities is not null)
        {
            await useCaseDispatcher.DispatchAsync(new UpdateProductFacilitiesUseCase(result, request.Facilities),
                cancellationToken);
        }

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
        }, cancellationToken);

        return Result<int>.Success(result);
    }
}