using Application.Models.Identity;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(GetTokenDto dto)
        {
            return Ok(await _mediator.Send(new GetTokenQuery(dto)));
        }
    }
}
