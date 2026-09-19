

using GamingForum.Application.Models.Requests.AuthRequests;
using GamingForum.Application.Models.Responses;
using GamingForum.Application.Services.Interfaces;
using GamingForum.Domain.Entities.Identity;
using GamingForum.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace GamingForum.Infrastructure.Auth
{
    public sealed class AuthService(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IJwtService jwtService,
        AppDbContext context,
        IOptions<JwtOptions> jwtOptions,
        TimeProvider timeProvider) : IAuthService
    {
        private readonly JwtOptions _jwt = jwtOptions.Value;
        public Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync(RefreshRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> RefreshAsync(RefreshRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        private async Task<AuthResponse> CreateAuthResponseAsync(AppUser user, CancellationToken ct) 
        {
            var roles = await userManager.GetRolesAsync(user);
            var accessToken = jwtService.GenerateToken(user, roles);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiresAt = timeProvider.GetUtcNow().UtcDateTime.AddDays(_jwt.RefreshTokenDurationInDays);

            

        }

        private static string GenerateRefreshToken() => Base64Url.EncodeToString(RandomNumberGenerator.GetBytes(64));
        private static string Hash(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }
}
