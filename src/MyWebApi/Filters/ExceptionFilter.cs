using Application.Exceptions;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (type == typeof(CustomNotFoundException))
            {
                var exception = (CustomNotFoundException)context.Exception;
                context.Result = new NotFoundObjectResult(new CustomActionResult<object>(
                    success: false,
                    message: exception.Message
                ));
                context.ExceptionHandled = true;
            }
            else if (type == typeof(ValidationException))
            {
                var exception = (ValidationException)context.Exception;
                context.Result = new BadRequestObjectResult(new CustomActionResult<object>(
                    success: false,
                    message: "Validation failed."
                ));
                context.ExceptionHandled = true;
            }
            else if (type == typeof(CustomException))
            {
                var exception = (CustomException)context.Exception;
                context.Result = new BadRequestObjectResult(new CustomActionResult<object>(
                    success: false,
                    message: exception.Message
                ));
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new BadRequestObjectResult(new CustomActionResult<object>(
                    success: false,
                    message: "Unhandled error occurred in server."
                ));
            }
            base.OnException(context);
        }
    }
}
