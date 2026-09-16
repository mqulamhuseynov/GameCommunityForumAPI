using GamingForum.Domain.Entities.Commons;


namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Game : BaseEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? CoverImageUrl { get; set; }
        public ICollection<Forum> Forums { get; set; } = [];
    }
}
