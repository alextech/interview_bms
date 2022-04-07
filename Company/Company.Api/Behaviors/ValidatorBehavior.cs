using Company.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Company.Api.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        List<ValidationFailure> failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {

            // can write to log

            // newline formatting is not easy to handle by consumer. Should have a convenient list of errors.
            string validationMessages = failures.Aggregate("\n", (current, failure) => current + (failure.ErrorMessage += "\n"));
            // throwing exceptions for domain errors is not best practice. Should create response types with status details.
            throw new CompanyDomainException(
                $"Command Validation Errors for type {typeof(TRequest).Name}: {validationMessages}",
                new ValidationException("Validation exception", failures));
        }

        return await next();
    }
}