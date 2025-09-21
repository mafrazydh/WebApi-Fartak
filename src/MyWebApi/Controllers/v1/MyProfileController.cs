using Application.Contracts;
using Application.Models.Users;
using Application.Wrappers;
using Domin.Entities;
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
    public class MyProfileController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment environment;
        private readonly IMediator mediator;

        public MyProfileController(IUserRepository userRepository, IWebHostEnvironment environment, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.environment = environment;
            this.mediator = mediator;
        }

        private const string ImagePath = "images/user";
        private string uploadRootpath = "";


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این یوزر وجود ندارد"));             
            }
            var result = await userRepository.GetUserById(userIdClaim.Value);
            return Ok(result);
        }


        [ExceptionFilter]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMyProfile(UserDto userDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این یوزر وجود ندارد"));
            }
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
            var result = await userRepository.UpdateAsync(userIdClaim.Value, userDto);

            return Ok(result);
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleyeMyAccount()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); 
            if (userIdClaim == null)
            {
                return Ok(new CustomActionResult<UserDto>(true, "این یوزر وجود ندارد"));
            }
            var result = await userRepository.DeleteUser(userIdClaim.Value);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Role>>> MyRoles()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                var result = await userRepository.UserRoles(userIdClaim.Value);
                return Ok(result);

            }
                return Ok(new CustomActionResult<UserDto>(true, "این یوزر وجود ندارد"));

        }
    }
}
