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
    public class OrderController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        private readonly IMediator mediator;

        public OrderController(IWebHostEnvironment environment, IMediator mediator)
        {
            this.environment = environment;
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> AllOrders()
        {
            var result = await mediator.Send(new AllOrdersQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderFromCarts(string userId)
        {


            try
            {
                var result = await  mediator.Send(new AddOrderFromCartsCommand(userId));
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
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> AllUserOrder(string userId)
        {
            var result = await mediator.Send(new AllUserOrderQuery(userId));

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این یوزر وجود ندارد"));
            }
            var result = await mediator.Send(new AllUserOrderQuery(userIdClaim.Value));

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            var result = mediator.Send(new DeleteOrderCommand(orderId));
            return Ok(result);
        }

    
        [HttpPut]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> ChangeOrderState(string orderId)
        {


            try
            {
                var result = await mediator.Send(new ChangeOrderStateCommand(orderId));
                return Ok(new CustomActionResult<object>(true, "", result));
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
    }
}
