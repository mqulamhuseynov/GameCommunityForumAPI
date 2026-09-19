using GamingForum.Application.Exceptions.BaseException;


namespace GamingForum.Application.Exceptions.ApplicationExceptions
{
    public sealed class UnauthorizedException(string message) : AppException(message)
    {
    }
}
