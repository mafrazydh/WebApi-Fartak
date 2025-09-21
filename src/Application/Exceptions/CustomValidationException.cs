namespace Application.Exceptions;

public class CustomValidationException : Exception
{
    public List<string> Errors { get; }

    public CustomValidationException(IEnumerable<FluentValidation.Results.ValidationFailure> failures)
        : base("Validation failed")
    {
        Errors = failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}").ToList();
    }
}