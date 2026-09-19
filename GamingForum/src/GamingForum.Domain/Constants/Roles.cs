
namespace GamingForum.Domain.Constants
{
    public static class Roles
    {
        public const string Admin  = "Admin";
        public const string Moderator = "Moderator";
        public const string User = "User";

        public static readonly string[] All = [Admin, Moderator, User]; 
    }
}
