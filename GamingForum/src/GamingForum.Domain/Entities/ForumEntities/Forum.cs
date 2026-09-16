using GamingForum.Domain.Entities.Commons;
using GamingForum.Domain.Enums;


namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Forum : BaseEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public ForumType Type { get; set; }
        public int DisplayOrder { get; set; }

        public Guid? GameId { get; set; }  //general forumlar ucun null, oyunlar ucun mutleq
        public Game? Game { get; set; }

        public ICollection<Topic> Topics { get; set; } = [];
    }
}
