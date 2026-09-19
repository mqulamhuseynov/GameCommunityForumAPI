namespace GamingForum.Application.Models.Requests.AuthRequests
{
    public sealed record RegisterRequest(string UserName, string Email, string Password);
}
