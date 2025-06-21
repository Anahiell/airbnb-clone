using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.Additional;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Additional;

public record UpdateProductAdditionalInfoUseCase(int ProductId, CancelPolicyEntityInfo? CancelPolicy, List<string>? HomeRules, List<string>? SafetyRules) : IUseCase<Result<string>>;

public class UpdateProductAdditionalInfoUseCaseHandler : IUseCaseHandler<UpdateProductAdditionalInfoUseCase, Result<string>>
{
    private readonly IAdditionalRepository _additionalRepository;
    private readonly IMediator _mediator;

    public UpdateProductAdditionalInfoUseCaseHandler(IAdditionalRepository additionalRepository, IMediator mediator)
    {
        _additionalRepository = additionalRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateProductAdditionalInfoUseCase request, CancellationToken cancellationToken)
    {
        var cancelPolicy = request.CancelPolicy is not null
            ? new CancelPolicy(request.CancelPolicy.FreeCancelationDays, request.CancelPolicy.PartCancelationDays, request.CancelPolicy.PartCancelationPercent)
            : null;

        var homeRules = request.HomeRules?.Select(r => new HomeRule(r)).ToList() ?? new List<HomeRule>();
        var safetyRules = request.SafetyRules?.Select(r => new SafetyRule(r)).ToList() ?? new List<SafetyRule>();

        var additional = await _additionalRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (additional == null)
        {
            additional = new Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates.Additional(
                request.ProductId, cancelPolicy, homeRules, safetyRules);

            await _additionalRepository.AddAsync(additional, cancellationToken);
        }
        else
        {
            additional.Update(cancelPolicy, homeRules, safetyRules);

            await _additionalRepository.UpdateAsync(additional, cancellationToken);
        }

        var evt = new AdditionalUpdatedEvent(request.ProductId, cancelPolicy, homeRules, safetyRules);
        await _mediator.Publish(evt, cancellationToken);

        return Result<string>.Success("Доп. информация обновлена");
    }
}