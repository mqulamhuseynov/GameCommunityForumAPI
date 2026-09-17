using GamingForum.Domain.Entities.Commons;


namespace GamingForum.Domain.Entities.Identity
{
    public class UserFollow : IAuditableEntity
    {
        public Guid FollowerId { get; set; }
        public AppUser Follower { get; set; } = null!;

        public Guid FollowedId { get; set; }
        public AppUser Followed { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
