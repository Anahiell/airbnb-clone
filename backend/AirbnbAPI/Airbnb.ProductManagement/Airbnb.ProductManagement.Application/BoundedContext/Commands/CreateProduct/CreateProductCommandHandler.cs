using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
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
    IMediator mediator,
    ICoordinateRepository coordinateRepository,
    IRuleRepository ruleRepository,
    IAdditionalRepository additionalRepository,
    IAdvantageRepository advantageRepository,
    IFeatureRepository featureRepository,
    IProductFeatureRepository productFeatureRepository,
    IFacilityRepository facilityRepository,
    IProductFacilityRepository productFacilityRepository)
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
        
        // Координаты
        if (request.Longitude is not null && request.Latitude is not null)
        {
            await coordinateRepository.DeleteAsync(c => c.ProductId == product.Id, cancellationToken);
            var coordinate = new Coordinate(request.Latitude, request.Longitude, product.Id);
            await coordinateRepository.AddAsync(coordinate, cancellationToken);
        }
        
        // --- Правила ---
        if (request.GuestRules is not null)
        {
            await ruleRepository.DeleteAsync(r => r.ProductId == product.Id, cancellationToken);
            var rules = new Rule(product.Id, request.GuestRules.MaxGuestsNumber, request.GuestRules.PetsAllowed,
                request.GuestRules.MaxPetsNumber, request.GuestRules.PetsAddedPrice);
            await ruleRepository.AddAsync(rules, cancellationToken);
        }

        // --- Дополнительная информация ---
        if (request.CancelPolicy is not null || request.HomeRules?.Any() == true || request.SafetyRules?.Any() == true)
        {
            await additionalRepository.DeleteAsync(a => a.ProductId == product.Id, cancellationToken);

            var cancelPolicy = request.CancelPolicy is not null
                ? new CancelPolicy(request.CancelPolicy.FreeCancelationDays, request.CancelPolicy.PartCancelationDays,
                    request.CancelPolicy.PartCancelationPercent)
                : null;

            var homeRules = request.HomeRules?
                .Select(r => new HomeRule(r))
                .ToList();

            var safetyRules = request.SafetyRules?
                .Select(r => new SafetyRule(r))
                .ToList();

            var additional = new Additional(product.Id, cancelPolicy, homeRules, safetyRules);
            await additionalRepository.AddAsync(additional, cancellationToken);
        }

        // --- Преимущества ---
        if (request.Advantages?.Any() == true)
        {
            await advantageRepository.DeleteAsync(a => a.ProductId == product.Id, cancellationToken);

            foreach (var item in request.Advantages.DistinctBy(x => new { x.Title, x.Description }))
            {
                var advantage = new Advantage(product.Id, item.Title, item.Description);
                await advantageRepository.AddAsync(advantage, cancellationToken);
            }
        }
        
        // --- Фичи ---
        var featureNames = request.Features ?? Enumerable.Empty<string>();
        var features = new List<Feature>();
        foreach (var name in featureNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var f = await featureRepository.GetByNameAsync(name, cancellationToken);
            if (f is not null) features.Add(f);
        }

        var missingFeatures = featureNames.Except(features.Select(f => f.Name)).ToList();
        if (missingFeatures.Any())
        {
            return Result<int>.Failure($"Фичи не найдены: {string.Join(", ", missingFeatures)}");
        }

        await productFeatureRepository.DeleteAllByProductIdAsync(product.Id, cancellationToken);
        foreach (var feature in features)
        {
            var pf = new ProductFeature(product.Id, feature.Id);
            await productFeatureRepository.AddAsync(pf, cancellationToken);
        }

        // --- Удобства ---
        var facilityNames = request.Facilities ?? Enumerable.Empty<string>();
        var facilities = new List<Facility>();
        foreach (var name in facilityNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var f = await facilityRepository.GetByNameAsync(name, cancellationToken);
            if (f is not null) facilities.Add(f);
        }

        var missingFacilities = facilityNames.Except(facilities.Select(f => f.Name)).ToList();
        if (missingFacilities.Any())
        {
            return Result<int>.Failure($"Удобства не найдены: {string.Join(", ", missingFacilities)}");
        }

        await productFacilityRepository.DeleteAllByProductIdAsync(product.Id, cancellationToken);
        foreach (var facility in facilities)
        {
            var pf = new ProductFacility(product.Id, facility.Id);
            await productFacilityRepository.AddAsync(pf, cancellationToken);
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
