
namespace GamingForum.Application.Models.Responses
{
    public sealed record AuthResponse(
        string AccessToken,
        DateTime AccessTokenExpiresAt,
        string RefreshToken,
        DateTime RefreshTokenExpiresAt);
}
