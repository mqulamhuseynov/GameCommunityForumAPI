using GamingForum.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;


namespace GamingForum.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, IAuditableEntity
    {
        public string? AvatarUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? Bio { get; set; }

        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public int TopicCount { get; set; }
        public int CommentCount { get; set; }

        public DateTime? BannedUntil { get; set; }
        public string? BanReason { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsBanned => BannedUntil is not null && BannedUntil > DateTime.UtcNow;
    }
}
