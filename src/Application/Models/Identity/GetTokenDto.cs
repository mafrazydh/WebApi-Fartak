namespace Application.Models.Identity
{
    public class GetTokenDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
