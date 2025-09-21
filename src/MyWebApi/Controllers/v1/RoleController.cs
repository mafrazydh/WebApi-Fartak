using Application.Commands;
using Application.Exceptions;
using Application.Models.Users;
using Application.Queries;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyWebApi.Filters;

namespace MyWebApi.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [CustomActionResultFilter]
    [ExceptionFilter]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {

        private readonly IMediator mediator;

        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AllRoles()
        {
            var result = await mediator.Send(new AllRolesQuery());

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(RoleDto dto)
        {


            try
            {
                var result = await mediator.Send(new CreateRoleCommand(dto));
                return Ok(new CustomActionResult<RoleDto>(true, result.Value));
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var result = await mediator.Send(new DeleteRoleCommand(Id));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleById([FromQuery]string Id)
        {
            var result = await mediator.Send(new GetRoleByIdQuery(Id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(string userid, string roleId)
        {
            var result = await mediator.Send(new AddRoleToUserCommand(userid, roleId));

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoleFromUserCommand(string userid, string roleId)
        {
            var result = await mediator.Send(new DeleteRoleFromUserCommand(userid, roleId));

            return Ok(result);
        }
    }
}
