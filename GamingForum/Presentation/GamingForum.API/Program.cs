using GamingForum.API.Filters;
using GamingForum.Infrastructure.Seed;
using Scalar.AspNetCore;
namespace GamingForum.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddAppDI(builder.Configuration);
            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());

            builder.Services.AddOpenApi();

            var app = builder.Build();

            await IdentitySeeder.SeedRolesAsync(app.Services);

            app.UseExceptionHandler();
            app.UseStatusCodePages();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
