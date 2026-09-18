using GamingForum.Domain.Entities.ForumEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingForum.Infrastructure.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Content).IsRequired().HasMaxLength(20_000);

            builder.Property(t => t.LastActivityAt).HasDefaultValueSql("now()");

            builder.HasOne(t => t.Forum)
                .WithMany(f => f.Topics)
                .HasForeignKey(t => t.ForumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Author)
                .WithMany()
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.ForumId, t.IsPinned, t.LastActivityAt })
                .IsDescending(false, true, true);

            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}
