using Application.Commands;
using Application.Exceptions;
using Application.Queries;
using Application.Wrappers;
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
    [Authorize(Roles = "Admin")]
    public class CartController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        private readonly IMediator mediator;

        public CartController(IWebHostEnvironment environment, IMediator mediator)
        {
            this.environment = environment;
            this.mediator = mediator;
        }

       
      
        [HttpPost]
        public async Task<IActionResult> AddToCart(string userId,string productId,int quantity)
        {


            try
            {
                var result =  mediator.Send(new AddToCartCommand(userId,productId,quantity));
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
        public async Task<IActionResult> GetCart(string userId)
        {
            var result = await mediator.Send(new GetCartQuery(userId));

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFromCart(string userId,string productId)
        {
            var result = await mediator.Send(new DeleteFromCartCommand(userId, productId));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartQuantity(string userId, string productId,int newQuantity)
        {
            var result = await mediator.Send(new UpdateCartQuantityCommand(userId, productId,newQuantity));
            return Ok(result);
        }

    }
}
