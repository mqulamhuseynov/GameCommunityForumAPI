using GamingForum.Application.IRepo.UoW;
using GamingForum.Domain.Entities.Identity;
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
            // Fail-fast: connection string yoxdursa, app açılışda aydın mesajla dayansın
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "ConnectionStrings:DefaultConnection tapılmadı. appsettings.Development.json və ya user-secrets yoxlayın.");

            service.TryAddSingleton(TimeProvider.System);
            service.AddScoped<AuditableEntityInterceptor>();

            service.AddDbContext<AppDbContext>((sp, options) =>
                options.UseNpgsql(connectionString)
                       .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>()));

            service.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

            // AddDefaultTokenProviders (email confirm / password reset token-ları) Data Protection tələb edir.
            // Bu olmasa app startup-da DI validation xətası ilə çökür.
            // Qeyd: production-da key-ləri paylaşılan yerdə saxla (PersistKeysToFileSystem / DB),
            // yoxsa restart-dan sonra köhnə reset linkləri etibarsız olur.
            service.AddDataProtection();

            // JWT scheme-i API qatında qurulacaq; burada yalnız authentication servisləri qeyd olunur,
            // çünki SignInManager IAuthenticationSchemeProvider tələb edir.
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

                // Email unikal olmalıdır: email ilə login və password reset buna bağlıdır
                options.User.RequireUniqueEmail = true;

                // Email göndərən servis hazır olanda true et
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                // SignInManager olmadan yuxarıdaki Lockout parametrləri işə düşmür:
                // login-də CheckPasswordSignInAsync(user, password, lockoutOnFailure: true) çağır.
                .AddSignInManager()
                .AddDefaultTokenProviders();

            return service;
        }
    }
}
