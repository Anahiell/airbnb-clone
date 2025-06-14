using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.DeleteFacilityCommand;

public class DeleteFacilityCommandHandler(IFacilityRepository facilityRepository, IMediator mediator)
    : ICommandHandler<DeleteFacilityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await facilityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (facility is null)
            return Result<string>.Failure("Удобство не найдено");

        await facilityRepository.DeleteAsync(facility.Id, cancellationToken);

        await mediator.Publish(new FacilityDeletedEvent(facility.Id), cancellationToken);

        return Result<string>.Success("Удобство успешно удалено");
    }
}