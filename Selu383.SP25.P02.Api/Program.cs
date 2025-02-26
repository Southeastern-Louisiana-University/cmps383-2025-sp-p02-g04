using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Data;
using Selu383.SP25.P02.Api.Features.Roles;
using Selu383.SP25.P02.Api.Features.Users;

namespace Selu383.SP25.P02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = context => {


                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    
                    return Task.CompletedTask;

                };
                options.Events.OnRedirectToLogin = context =>
                {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return Task.CompletedTask;
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                await db.Database.MigrateAsync();
                await SeedRoles.Initialize(scope.ServiceProvider);
                await SeedUsers.Initialize(scope.ServiceProvider, userManager, roleManager);
                SeedTheaters.Initialize(scope.ServiceProvider);
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting()
             .UseEndpoints(x =>
                {
                    x.MapControllers();
                });

            app.UseStaticFiles();


            if (app.Environment.IsDevelopment())
            {
                app.UseSpa(x =>
                {
                    x.UseProxyToSpaDevelopmentServer("http://localhost:5173");
                });
            }
            else
            {
                app.MapFallbackToFile("/index.html");
            }


            app.Run();
        }
    }
}

