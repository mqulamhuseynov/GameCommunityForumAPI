

namespace GamingForum.Domain.Entities.Commons
{
    // Remove() çağırılanda interceptor sətri silmir, IsDeleted = true edir
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
