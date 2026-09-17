using GamingForum.Domain.Entities.Commons;


namespace GamingForum.Domain.Entities.ForumEntities
{
    public class Forum : BaseEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // null = general forum, dəyər varsa oyuna bağlı forum
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }

        public ICollection<Topic> Topics { get; set; } = [];
    }
}
