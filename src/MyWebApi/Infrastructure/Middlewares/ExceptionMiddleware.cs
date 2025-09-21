using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var response = new { errors = ex.Errors };
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (CustomNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                var problem = new ProblemDetails
                {
                    Title = "منبع یافت نشد",
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://yourdomain.com/errors/{ex.EntityName.ToLower()}-not-found"
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var problem = new ProblemDetails
                {
                    Title = ex.Title,
                    Detail = ex.Message,
                    Status = ex.StatusCode,
                    Type = ex.Type
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var problem = new ProblemDetails
                {
                    Title = "خطای داخلی سرور",
                    Detail = ex.Message,
                    Status = 500,
                    Type = "https://yourdomain.com/errors/internal"
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
