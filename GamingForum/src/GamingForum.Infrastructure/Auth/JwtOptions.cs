
using System.ComponentModel.DataAnnotations;


namespace GamingForum.Infrastructure.Auth
{
    public sealed class JwtOptions
    {
        public const string SectionName = "JwtSettings";

        [Required, MinLength(32)]
        public string Key { get; init; } = null!;

        [Required]
        public string Issuer { get; init; } = null!;

        [Required]
        public string Audience { get; init; } = null!;

        [Range(1, 1440)]
        public int DurationInMinutes { get; init; }
        [Range(1,90)]
        public int RefreshTokenDurationInDays { get; init; }
    }
}
