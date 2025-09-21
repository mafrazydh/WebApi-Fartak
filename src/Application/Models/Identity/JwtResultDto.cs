namespace Application.Models.Identity
{
    public class JwtResultDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
