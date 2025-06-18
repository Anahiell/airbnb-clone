using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Events;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Facility;

public record UpdateProductFacilitiesUseCase(int ProductId, IEnumerable<string>? FacilityNames)
    : IUseCase<Result<string>>;

public class UpdateProductFacilitiesUseCaseHandler : IUseCaseHandler<UpdateProductFacilitiesUseCase, Result<string>>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IProductFacilityRepository _productFacilityRepository;
    private readonly IUseCaseDispatcher _dispatcher;
    private readonly IMediator _mediator;


    public UpdateProductFacilitiesUseCaseHandler(IFacilityRepository facilityRepository,
        IProductFacilityRepository productFacilityRepository,
        IUseCaseDispatcher dispatcher, IMediator mediator)
    {
        _facilityRepository = facilityRepository;
        _productFacilityRepository = productFacilityRepository;
        _dispatcher = dispatcher;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateProductFacilitiesUseCase request,
        CancellationToken cancellationToken)
    {
        var facilityNames = request.FacilityNames ?? Enumerable.Empty<string>();

        var facilities = new List<Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates.Facility>();
        foreach (var name in facilityNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var facility = await _facilityRepository.GetByNameAsync(name, cancellationToken);
            if (facility is not null)
                facilities.Add(facility);
        }

        var missing = facilityNames.Except(facilities.Select(f => f.Name), StringComparer.OrdinalIgnoreCase).ToList();
        if (missing.Any())
        {
            return Result<string>.Failure($"Удобства не найдены: {string.Join(", ", missing)}");
        }

        await _productFacilityRepository.DeleteAllByProductIdAsync(request.ProductId, cancellationToken);

        var createdFacilities = new List<(int Id, string Name, string IconName)>();
        foreach (var facility in facilities)
        {
            var pf = new ProductFacility(request.ProductId, facility.Id);
            await _productFacilityRepository.AddAsync(pf, cancellationToken);

            createdFacilities.Add((facility.Id, facility.Name, facility.IconName));
        }
        
        if (createdFacilities.Count > 0)
        {
            var evt = new ProductFacilitiesUpdatedEvent(request.ProductId, createdFacilities);
            await _mediator.Publish(evt, cancellationToken);
        }

        return Result<string>.Success("Удобства успешно обновлено");
    }
}