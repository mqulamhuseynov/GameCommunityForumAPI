
using GamingForum.Application.Models.Requests.AuthRequests;
using GamingForum.Application.Models.Responses;

namespace GamingForum.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct);
        Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct);
        Task<AuthResponse> RefreshAsync(RefreshRequest request, CancellationToken ct);
        Task LogoutAsync(RefreshRequest request, CancellationToken ct);
    }
}
