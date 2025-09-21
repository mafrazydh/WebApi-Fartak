namespace Application.Models.Identity
{
    public class JwtOption
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int Expiration { get; set; }
    }
}
