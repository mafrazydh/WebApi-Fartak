using Application.Exceptions;
using Application.Models.Identity;
using Domin.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class JwtTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOption _jwtOption;
        private readonly ApplicationDbContext _context;
        public JwtTokenService(UserManager<User> userManager, IOptions<JwtOption> jwtOption, ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwtOption = jwtOption.Value;
            _context = context;
        }
        public async Task<JwtResultDto> GenerateTokenAsync(GetTokenDto dto)
        {
            var user = _context.Users.Where(d=>d.UserName == dto.UserName).SingleOrDefault();
            if (user is null)
            {
                throw new CustomException("username or password is incorrect");
            }
            if (await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var roleCliams = new List<Claim>();
                foreach (var role in roles)
                {
                    roleCliams.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtCliams = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName,user.FullName),
                    new Claim("CustomClaim","CustomValue")
                };
                var claims = userClaims.Union(jwtCliams).Union(roleCliams);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key));
                var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var expireTime = DateTime.UtcNow.AddMinutes(_jwtOption.Expiration);

                var jwtSecurotyToken = new JwtSecurityToken(
                    issuer: _jwtOption.Issuer,
                    audience: _jwtOption.Audience,
                    expires: expireTime,
                    signingCredentials: signingCredential,
                    claims: claims
                    );
                return new JwtResultDto
                {
                    Expiration = expireTime,
                    UserId = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurotyToken)
                };
            }
            throw new CustomException("username or password is incorrect");
        }
    }
}
