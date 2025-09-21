using Application.Exceptions;
using MediatR;


public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (FluentValidation.ValidationException ex)
        {
            // مدیریت خطا
            throw new CustomValidationException(ex.Errors);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
