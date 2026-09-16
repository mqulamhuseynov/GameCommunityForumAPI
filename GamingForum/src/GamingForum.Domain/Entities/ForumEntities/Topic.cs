using GamingForum.Domain.Entities.Commons;

namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;

        public Guid ForumId { get; set; }
        public Forum Forum { get; set; } = null!;

        public Guid? GameId { get; set; } 
        public Game Game { get; set; }

        public ICollection<Comment> Comments { get; set; } = [];
    }
}
