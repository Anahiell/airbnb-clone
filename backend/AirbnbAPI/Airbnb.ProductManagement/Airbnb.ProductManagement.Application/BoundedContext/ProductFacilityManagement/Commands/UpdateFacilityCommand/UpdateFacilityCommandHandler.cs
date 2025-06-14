using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.UpdateFacilityCommand;

public class UpdateFacilityCommandHandler(IFacilityRepository facilityRepository, IMediator mediator)
    : ICommandHandler<UpdateFacilityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await facilityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (facility is null)
            return Result<string>.Failure("Удобство не найдено");

        facility.Update(request.IconName, request.Name);

        await facilityRepository.UpdateAsync(facility, cancellationToken);

        await mediator.Publish(new FacilityUpdatedEvent(facility.Id, facility.IconName, facility.Name), cancellationToken);

        return Result<string>.Success("Удобство успешно обновлено");
    }
}