using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.CreateFacilityCommand;

public class CreateFacilityCommandHandler(IFacilityRepository facilityRepository, IMediator mediator)
    : ICommandHandler<CreateFacilityCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = new Facility(request.IconName, request.Name);
        var result = await facilityRepository.AddAsync(facility, cancellationToken);

        await mediator.Publish(new FacilityCreatedEvent(facility.Id, facility.IconName, facility.Name), cancellationToken);

        return Result<int>.Success(result);
    }
}