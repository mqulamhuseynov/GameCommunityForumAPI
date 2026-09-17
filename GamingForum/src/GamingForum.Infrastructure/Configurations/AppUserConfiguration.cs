using GamingForum.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingForum.Infrastructure.Configurations
{
    // AspNetUsers-i IdentityDbContext özü konfiqurasiya edir, burada yalnız AppUser-ə əlavə olunanlar var
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Ignore(u => u.IsBanned);

            builder.Property(u => u.AvatarUrl).HasMaxLength(500);
            builder.Property(u => u.BannerUrl).HasMaxLength(500);
            builder.Property(u => u.Bio).HasMaxLength(1000);
            builder.Property(u => u.BanReason).HasMaxLength(500);
        }
    }
}
