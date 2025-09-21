using Application.Commands;
using Application.Exceptions;
using Application.Models.Users;
using Application.Queries;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Filters;
using System.Security.Claims;

namespace MyWebApi.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [CustomActionResultFilter]
    [Authorize]
    public class MyCartController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        private readonly IMediator mediator;

        public MyCartController(IWebHostEnvironment environment, IMediator mediator)
        {
            this.environment = environment;
            this.mediator = mediator;
        }

       
      
        [HttpPost]
        public async Task<IActionResult> AddToMyCart(string productId,int quantity)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این یوزر وجود ندارد"));
            }

            try
            {
                var result =  mediator.Send(new AddToCartCommand(userIdClaim.Value,productId,quantity));
                return Ok(new CustomActionResult<object>(true,"",result ));
            }
            catch (CustomValidationException ex)
            {
                return BadRequest(new CustomActionResult<object>(false, ex.Message, errors: [.. ex.Errors.Select(e => e.ToString())]));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomActionResult<object>(false, "خطایی در پردازش درخواست به وجود آمد.", errors: new List<string> { ex.Message }));
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این کاربر وجود ندارد"));
            }
            var result = await mediator.Send(new GetCartQuery(userIdClaim.Value));

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFromCart(string productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این کاربر وجود ندارد"));
            }
            var result = await mediator.Send(new DeleteFromCartCommand(userIdClaim.Value, productId));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartQuantity(string productId,int newQuantity)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این کاربر وجود ندارد"));
            }
            var result = await mediator.Send(new UpdateCartQuantityCommand(userIdClaim.Value, productId,newQuantity));
            return Ok(result);
        }

    }
}
