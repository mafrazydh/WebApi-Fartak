using Microsoft.AspNetCore.Http;

namespace Application.Models.Users
{
    public class UserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public required string FullName { get; set; }
        public IFormFile? Profile  { get; set; }
        public string? ProfileStr { get; set; }
    }
}
