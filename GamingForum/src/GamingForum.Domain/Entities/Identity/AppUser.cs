using GamingForum.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;


namespace GamingForum.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, IAuditableEntity
    {
        public string? AvatarUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? Bio { get; set; }

        // ExecuteUpdateAsync ilə atomik artır, "oxu → +1 → yaz" etmə
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public int TopicCount { get; set; }
        public int CommentCount { get; set; }

        // LockoutEnd-dən ayrıdır: o, səhv parol cəhdlərinə görə avtomatik dolur.
        // null = ban yoxdur, daimi ban üçün uzaq tarix ver.
        public DateTime? BannedUntil { get; set; }
        public string? BanReason { get; set; }

        // qeydiyyat tarixi
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // AppUserConfiguration-da Ignore edilib, sütun yaranmır
        public bool IsBanned => BannedUntil is not null && BannedUntil > DateTime.UtcNow;
    }
}
