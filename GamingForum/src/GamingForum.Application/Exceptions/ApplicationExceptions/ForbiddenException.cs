

using GamingForum.Application.Exceptions.BaseException;

namespace GamingForum.Application.Exceptions.ApplicationExceptions
{
    public sealed class ForbiddenException(string message) : AppException(message)
    {
    }
}
