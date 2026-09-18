using GamingForum.Application.IRepo.UoW;
using GamingForum.Application.Services.Interfaces;
using GamingForum.Domain.Entities.Identity;
using GamingForum.Infrastructure.Auth;
using GamingForum.Infrastructure.Contexts;
using GamingForum.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GamingForum.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "ConnectionStrings:DefaultConnection tapılmadı. appsettings.Development.json və ya user-secrets yoxlayın.");

            service.TryAddSingleton(TimeProvider.System);
            service.AddScoped<AuditableEntityInterceptor>();

            service.AddDbContext<AppDbContext>((sp, options) =>
                options.UseNpgsql(connectionString)
                       .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>()));

            service.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

            service.AddDataProtection();

            service.AddAuthentication();

            service.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            service.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

            service.AddScoped<IJwtService, JwtService>();

            return service;
        }
    }
}
