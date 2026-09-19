
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GamingForum.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBizinisDI(this IServiceCollection service)
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            service.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return service;
        }
    }
}
