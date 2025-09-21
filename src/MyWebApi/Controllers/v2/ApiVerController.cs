using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ApiVerController : ControllerBase
    {
    }
}
