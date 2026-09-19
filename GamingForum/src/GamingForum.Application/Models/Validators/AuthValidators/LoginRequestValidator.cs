
using FluentValidation;
using GamingForum.Application.Models.Requests.AuthRequests;

namespace GamingForum.Application.Models.Validators.AuthValidators
{
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() 
        {
           RuleFor(x => x.Login).NotEmpty()
                .MaximumLength(256);
           
            RuleFor(x => x.Password).NotEmpty()
                .MaximumLength(128);
        }
    }
}
