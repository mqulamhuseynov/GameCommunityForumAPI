using GamingForum.Domain.Entities.Identity;

namespace GamingForum.Application.Services.Interfaces
{
    public sealed record AccessToken(string Token, DateTime ExpiresAt);
    public interface IJwtService
    {
      AccessToken GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}