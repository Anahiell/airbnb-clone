using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Advantage;

public record UpdateProductAdvantagesUseCase(int ProductId, List<Advantages> Advantages) : IUseCase<Result<string>>;

public class UpdateProductAdvantagesUseCaseHandler : IUseCaseHandler<UpdateProductAdvantagesUseCase, Result<string>>
{
    private readonly IAdvantageRepository _advantageRepository;
    private readonly IMediator _mediator;

    public UpdateProductAdvantagesUseCaseHandler(IAdvantageRepository advantageRepository)
    {
        _advantageRepository = advantageRepository;
    }

    public async Task<Result<string>> Handle(UpdateProductAdvantagesUseCase request, CancellationToken cancellationToken)
    {
        await _advantageRepository.DeleteAsync(a => a.ProductId == request.ProductId, cancellationToken);

        var createdAdvantages = new List<(int Id, string Title, string Description)>();

        foreach (var item in request.Advantages.DistinctBy(x => new { x.Title, x.Description }))
        {
            var advantage = new Domain.BoundedContexts.ProductAdvantageManagement.Aggregates.Advantage(request.ProductId, item.Title, item.Description);
            await _advantageRepository.AddAsync(advantage, cancellationToken);
            
            createdAdvantages.Add((advantage.Id, advantage.Title, advantage.Description));
        }
        
        var evt = new ProductAdvantagesUpdatedEvent(request.ProductId, createdAdvantages);
        await _mediator.Publish(evt, cancellationToken);

        return Result<string>.Success("Преимущества обновлены");
    }
}