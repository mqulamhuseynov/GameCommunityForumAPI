using GamingForum.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingForum.Infrastructure.Configurations
{
    public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
    {
        public void Configure(EntityTypeBuilder<UserFollow> builder)
        {
            builder.HasKey(f => new { f.FollowerId, f.FollowedId });

            builder.HasOne(f => f.Follower)
                .WithMany()
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Followed)
                .WithMany()
                .HasForeignKey(f => f.FollowedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(f => f.FollowedId);

            builder.ToTable(t => t.HasCheckConstraint(
                "ck_user_follows_not_self",
                @"""FollowerId"" <> ""FollowedId"""));
        }
    }
}
