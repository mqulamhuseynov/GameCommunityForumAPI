using GamingForum.Application.Exceptions.BaseException;


namespace GamingForum.Application.Exceptions.ApplicationExceptions
{
    public sealed class ValidationFailedException(IDictionary<string, string[]> errors) : AppException("Validation(s) failed.")
    {
        public IDictionary<string, string[]> Errors { get; } = errors;
    }
}
