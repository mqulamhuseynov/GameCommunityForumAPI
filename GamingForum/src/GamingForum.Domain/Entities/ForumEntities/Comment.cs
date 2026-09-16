using GamingForum.Domain.Entities.Commons;


namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; } = default!;
    }
}
