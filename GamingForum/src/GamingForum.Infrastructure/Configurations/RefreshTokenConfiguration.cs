
using GamingForum.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingForum.Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Ignore(t => t.IsActive);

            builder.Property(t => t.TokenHash).IsRequired().HasMaxLength(64);
            builder.Property(t => t.ReplacedByTokenHash).HasMaxLength(64);

            builder.HasIndex(t => t.TokenHash).IsUnique();

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
