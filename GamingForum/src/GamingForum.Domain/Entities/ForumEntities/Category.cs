

using GamingForum.Domain.Entities.Commons;

namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public ICollection<Topic> Topics { get; set; } = [];
    }
}
