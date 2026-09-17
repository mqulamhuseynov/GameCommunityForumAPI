using GamingForum.Domain.Entities.Commons;
using GamingForum.Domain.Entities.Identity;


namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Comment : BaseEntity, ISoftDeletable
    {
        public required string Content { get; set; }

        public Guid TopicId { get; set; }
        public Topic Topic { get; set; } = null!;

        public Guid AuthorId { get; set; }
        public AppUser Author { get; set; } = null!;

        // null = topic-ə birinci səviyyə cavab
        public Guid? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = [];

        public bool IsEdited { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
