using Application.Contracts;
using Application.Exceptions;
using Application.Models.Users;
using Application.Wrappers;
using Domin.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Filters;
using System.Net.Mime;

namespace MyWebApi.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [CustomActionResultFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment environment;
        private readonly IMediator mediator;

        public UserController(IUserRepository userRepository, IWebHostEnvironment environment, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.environment = environment;
            this.mediator = mediator;
        }

        private const string ImagePath = "images/user";
        private string uploadRootpath = "";

        [ExceptionFilter]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDto dto)
        {


            try
            {
                uploadRootpath = Path.Combine(environment.WebRootPath, ImagePath);
                if (dto.Profile is not null && dto.Profile.Length > 0)
                {
                  
                        string fileExtension = Path.GetExtension(Path.GetFileName(dto.Profile.FileName));
                        string newFileName = $"Product_{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
                        var filePath = Path.Combine(uploadRootpath, newFileName);

                        using var fileStream = new FileStream(filePath, FileMode.Create);
                        await dto.Profile.CopyToAsync(fileStream).ConfigureAwait(false);             

                    dto.ProfileStr = $"/{ImagePath}/{newFileName}";

                }
                var result = await userRepository.CreateAsync(dto);
                return Ok(new CustomActionResult<UserDto>(true, result));
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
        public async Task<IActionResult> AllUsers()
        {
            var result = await userRepository.AllUsers();

            return Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetUserById(string Id)
        {
            var result = await userRepository.GetUserById(Id);

            return Ok(result);
        }


        [ExceptionFilter]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateAsync(string id, UserDto userDto)
        {
            var uploadRootpath = Path.Combine(environment.WebRootPath, ImagePath);
            if (userDto.Profile is not null && userDto.Profile.Length > 0)
            {
                string fileExtension = Path.GetExtension(Path.GetFileName(userDto.Profile.FileName));
                string newFileName = $"user_{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
                var filePath = Path.Combine(uploadRootpath, newFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await userDto.Profile.CopyToAsync(fileStream).ConfigureAwait(false);
                userDto.ProfileStr = $"/{ImagePath}/{newFileName}";

            }
            var result = await userRepository.UpdateAsync(id, userDto);

            return Ok(result);
        }


        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var result = await userRepository.DeleteUser(Id);

            return Ok(result);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<List<Role>>> UserRoles(string Id)
        {
            var result = await userRepository.UserRoles(Id);

            return Ok(result);
        }


        
    }
}
