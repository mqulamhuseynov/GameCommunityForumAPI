
using FluentValidation;
using GamingForum.Application.Models.Requests.AuthRequests;

namespace GamingForum.Application.Models.Validators.AuthValidators
{
    public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.UserName).NotEmpty()
                .Length(3, 20)
                .Matches("^[a-zA-Z0-9_]+$")
                .WithMessage("UserName can only contain letters, numbers, and underscores.");

            RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(128);
        }
    }
}
