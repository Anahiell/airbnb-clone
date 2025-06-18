using MediatR;

namespace Airbnb.Application.UseCases;

public interface IUseCaseDispatcher
{
    Task<TOut> DispatchAsync<TOut>(IUseCase<TOut> useCase, CancellationToken cancellationToken = default);

    Task DispatchAsync(IUseCase useCase, CancellationToken cancellationToken = default);
}

public interface IUseCaseHandler<in TIn, TOut>
    : IRequestHandler<TIn, TOut>
    where TIn : IUseCase<TOut>;

public interface IUseCaseHandler<in TIn>
    : IRequestHandler<TIn, EmptyUseCaseOutputDto>
    where TIn : IUseCase
{
    async Task<EmptyUseCaseOutputDto> IRequestHandler<TIn, EmptyUseCaseOutputDto>.Handle(TIn request,
        CancellationToken cancellationToken)
    {
        await Handle(request, cancellationToken).ConfigureAwait(false);

        return EmptyUseCaseOutputDto.Value;
    }

    new Task Handle(TIn request, CancellationToken cancellationToken);
}

public interface IUseCase
    : IUseCase<EmptyUseCaseOutputDto>;

public interface IUseCase<out TOut>
    : IRequest<TOut>;
    
public class EmptyUseCaseOutputDto
{
    public static readonly EmptyUseCaseOutputDto Value = new();
}


public class UseCaseDispatcher : IUseCaseDispatcher
{
    private readonly IMediator _mediator;

    public UseCaseDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TOut> DispatchAsync<TOut>(IUseCase<TOut> useCase, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(useCase, cancellationToken);
    }

    public async Task DispatchAsync(IUseCase useCase, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(useCase, cancellationToken);
    }
}