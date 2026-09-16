

using GamingForum.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamingForum.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            service.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            return service;
        }
    }
}
