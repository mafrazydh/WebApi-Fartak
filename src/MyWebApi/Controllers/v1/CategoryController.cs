using Application.Commands;
using Application.Models.Category;
using Application.Queries;
using Application.Wrappers;
using Domin.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Filters;

namespace MyWebApi.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [CustomActionResultFilter]
    [ExceptionFilter]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AllCategory()
        {
            var result = await mediator.Send(new AllCategoriesQuery());
            if (result.Count()==0)
            {
                return Ok(new CustomActionResult<object>(true, "هیچ محصولی موجود نیست", result));

            }
            return Ok(new CustomActionResult<object>(true, "", result));

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromQuery]CategoryDto dto)
        {     
            var result = mediator.Send(new CreateCategotyAsyncCommand(dto));
            if (result.IsCompletedSuccessfully == true)
            {
                return Ok(new CustomActionResult<object>(true,"",result.Result));
            }
            return Ok(new CustomActionResult<object>(true, "", result.Exception.InnerExceptions.Select(x => x.Message).ToList()));
           
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")] 
        public Task<IActionResult> DeleteProduct([FromQuery] string Id)
        {
            var result =  mediator.Send(new DeleteCategoryCommand(Id));

            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsInCategory([FromQuery]string categoryId)
        {
            var result = await mediator.Send(new GetCategoryInfoQuery(categoryId));
            
                return Ok(new CustomActionResult<Category>(true, "", result));

        }

    }
}
