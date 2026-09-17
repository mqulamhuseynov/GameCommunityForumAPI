

namespace GamingForum.Domain.Entities.Commons
{
    // AppUser IdentityUser<Guid>-dən miras aldığı üçün BaseEntity-dən miras ala bilmir
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
