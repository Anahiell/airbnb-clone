using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Rule;

public record UpdateProductGuestRulesUseCase(int ProductId, GuestRules GuestRules) : IUseCase<Result<string>>;

public class UpdateProductGuestRulesUseCaseHandler : IUseCaseHandler<UpdateProductGuestRulesUseCase, Result<string>>
{
    private readonly IRuleRepository _ruleRepository;
    private readonly IMediator _mediator;

    public UpdateProductGuestRulesUseCaseHandler(IRuleRepository ruleRepository, IMediator mediator)
    {
        _ruleRepository = ruleRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateProductGuestRulesUseCase request, CancellationToken cancellationToken)
    {
        await _ruleRepository.DeleteAsync(r => r.ProductId == request.ProductId, cancellationToken);

        var rules = new Domain.BoundedContexts.ProductRulesManagement.Aggregates.Rule(
            request.ProductId,
            request.GuestRules.MaxGuestsNumber,
            request.GuestRules.PetsAllowed,
            request.GuestRules.MaxPetsNumber,
            request.GuestRules.PetsAddedPrice
        );

        await _ruleRepository.AddAsync(rules, cancellationToken);

        var evt = new RuleUpdatedEvent(request.ProductId, request.GuestRules.MaxGuestsNumber,
            request.GuestRules.PetsAllowed, request.GuestRules.MaxPetsNumber, request.GuestRules.PetsAddedPrice);
        await _mediator.Publish(evt, cancellationToken);

        return Result<string>.Success("Правила гостей обновлены");
    }
}