using FluentValidation;
using MediatR;


namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .GroupBy(e => $"{e.PropertyName}-{e.ErrorMessage}-{e.Severity}")
            .Select(g => g.First())
            .Select(error => new 
            {
                Field = error.PropertyName,
                Message = error.ErrorMessage,
                Severity = error.Severity.ToString()
            })
            .ToList();

        if (failures.Any())
        {
                throw new FluentValidation.ValidationException(string.Join("\n", failures));
        }

            return await next();
        }
    }
}
