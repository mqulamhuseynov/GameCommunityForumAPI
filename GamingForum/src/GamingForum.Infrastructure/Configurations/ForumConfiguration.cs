using GamingForum.Domain.Entities.ForumEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingForum.Infrastructure.Configurations
{
    public class ForumConfiguration : IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.Property(f => f.Name).IsRequired().HasMaxLength(100);
            builder.Property(f => f.Slug).IsRequired().HasMaxLength(120);
            builder.Property(f => f.Description).HasMaxLength(500);

            builder.HasIndex(f => f.Slug).IsUnique();
            builder.HasIndex(f => new { f.CategoryId, f.DisplayOrder });

            builder.HasOne(f => f.Category)
                .WithMany(c => c.Forums)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Game)
                .WithMany(g => g.Forums)
                .HasForeignKey(f => f.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
