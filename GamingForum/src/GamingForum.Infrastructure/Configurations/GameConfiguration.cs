using GamingForum.Domain.Entities.ForumEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingForum.Infrastructure.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(g => g.Name).IsRequired().HasMaxLength(150);
            builder.Property(g => g.Slug).IsRequired().HasMaxLength(150);
            builder.Property(g => g.CoverImageUrl).HasMaxLength(500);

            builder.HasIndex(g => g.Slug).IsUnique();
        }
    }
}
