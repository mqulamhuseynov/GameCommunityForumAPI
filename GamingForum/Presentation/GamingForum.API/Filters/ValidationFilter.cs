using FluentValidation;
using GamingForum.Application.Exceptions.ApplicationExceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamingForum.API.Filters
{
    public sealed class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
                    continue;

                var result = await validator.ValidateAsync(
                    new ValidationContext<object>(argument),
                    context.HttpContext.RequestAborted);

                if (!result.IsValid)
                    throw new ValidationFailedException(result.ToDictionary());
            }

            await next();
        }
    }
}