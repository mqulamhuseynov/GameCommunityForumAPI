using GamingForum.Application.Services.Interfaces;
using GamingForum.Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace GamingForum.Infrastructure.Auth
{
    public sealed class JwtService(IOptions<JwtOptions> options, TimeProvider timeProvider) : IJwtService
    {
        private readonly JwtOptions _jwt = options.Value;

        public string GenerateToken(AppUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("name", user.UserName!)
            };
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var now = timeProvider.GetUtcNow().UtcDateTime;

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddMinutes(_jwt.DurationInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                    SecurityAlgorithms.HmacSha256)
            };

            return new JsonWebTokenHandler().CreateToken(descriptor);
        }
    }
}