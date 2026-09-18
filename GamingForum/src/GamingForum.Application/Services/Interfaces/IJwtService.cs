using GamingForum.Domain.Entities.Identity;

namespace GamingForum.Application.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}