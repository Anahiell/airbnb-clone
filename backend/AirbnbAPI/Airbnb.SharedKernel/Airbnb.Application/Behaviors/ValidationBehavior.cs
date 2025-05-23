﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

        if (failures.Any())
        {
            var exception = new CustomValidationException(failures.Select(f => f.ErrorMessage).ToList());

            logger.LogError("Completed request {RequestName} with errors {Errors}", typeof(TRequest).Name, failures);

            throw exception;
        }

        return await next();
    }
}