using GamingForum.Infrastructure;
using GamingForum.Application;
namespace GamingForum.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection service, IConfiguration configuration) 
        {
            service.AddInfraDI(configuration)
            .AddBizinisDI();
            return service;
        }
    }
}
