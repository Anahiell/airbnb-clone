using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Coordinates;

public record UpdateProductCoordinatesUseCase(int ProductId, string Latitude, string Longitude) : IUseCase<Result<string>>;

public class UpdateProductCoordinatesUseCaseHandler : IUseCaseHandler<UpdateProductCoordinatesUseCase, Result<string>>
{
    private readonly ICoordinateRepository _coordinateRepository;
    private readonly IMediator _mediator;

    public UpdateProductCoordinatesUseCaseHandler(ICoordinateRepository coordinateRepository, IMediator mediator)
    {
        _coordinateRepository = coordinateRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateProductCoordinatesUseCase request, CancellationToken cancellationToken)
    {
        await _coordinateRepository.DeleteAsync(c => c.ProductId == request.ProductId, cancellationToken);
        var coordinate = new Coordinate(request.Latitude, request.Longitude, request.ProductId);
        await _coordinateRepository.AddAsync(coordinate, cancellationToken);
        
        var evt = new CoordinateUpdatedEvent(request.ProductId, request.Latitude, request.Longitude);
        await _mediator.Publish(evt, cancellationToken);
        
        return Result<string>.Success("Координаты обновлены");
    }
}