using GamingForum.Application.Exceptions.BaseException;


namespace GamingForum.Application.Exceptions.ApplicationExceptions
{
    public sealed class NotFoundException(string message) : AppException(message)
    {
    }
}
