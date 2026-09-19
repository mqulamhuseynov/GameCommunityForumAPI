

using GamingForum.Application.Exceptions.BaseException;

namespace GamingForum.Application.Exceptions.ApplicationExceptions
{
    public sealed class ConflictException(string message) : AppException(message)
    {
    }
}
