using GamingForum.Domain.Entities.Commons;
using GamingForum.Domain.Entities.Identity;


namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Topic : BaseEntity, ISoftDeletable
    {
        public required string Title { get; set; }
        public required string Content { get; set; }

        public Guid AuthorId { get; set; }
        public AppUser Author { get; set; } = null!;

        public Guid ForumId { get; set; }
        public Forum Forum { get; set; } = null!;

        public bool IsPinned { get; set; }
        public bool IsLocked { get; set; }
        public int ViewCount { get; set; }

        public DateTime LastActivityAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Comment> Comments { get; set; } = [];
    }
}
