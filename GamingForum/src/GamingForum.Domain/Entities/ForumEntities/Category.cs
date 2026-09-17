using GamingForum.Domain.Entities.Commons;


namespace GamingForum.Domain.Entities.ForumEntities
{
    // Ana səhifə bölmələri. Topic Category-yə deyil, yalnız Forum-a bağlıdır.
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }

        public ICollection<Forum> Forums { get; set; } = [];
    }
}
