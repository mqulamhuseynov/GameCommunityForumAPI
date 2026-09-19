
using FluentValidation;
using GamingForum.Application.Models.Requests.AuthRequests;

namespace GamingForum.Application.Models.Validators.AuthValidators
{
    public sealed class RefreshRequestValidator : AbstractValidator<RefreshRequest>
    {
       public RefreshRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().MaximumLength(200);
        } 
    }
}
