using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApi.Filters
{
    public class CustomActionResultFilter : ActionFilterAttribute
    {


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                if (context.Result is ObjectResult obj)
                {
                    context.Result = new OkObjectResult(new CustomActionResult<object>(
                        success: obj.StatusCode.HasValue,
                        message: "عملیات با موفقیت انجام شد.",
                        result: obj.Value
                    ));
                }
                else if (context.ModelState.IsValid)
                {
                    context.Result = new OkObjectResult(new CustomActionResult<object>(
                        success: true,
                        message: "عملیات با موفقیت انجام شد."
                    ));
                }
                else
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    context.Result = new BadRequestObjectResult(new CustomActionResult<object>(
                        success: false,
                        message: "خطا در اعتبارسنجی.",
                        errors: errors
                    ));
                }
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                // ثبت خطاهای اعتبارسنجی
                context.Result = new BadRequestObjectResult(new CustomActionResult<object>(
                    success: false,
                    message: "خطا در اعتبارسنجی.",
                    errors: validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }
            catch (Exception ex)
            {
                // ثبت خطاهای دیگر
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            base.OnActionExecuted(context);
        }


    }
}