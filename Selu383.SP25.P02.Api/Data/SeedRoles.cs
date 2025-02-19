using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Features.Roles;

namespace Selu383.SP25.P02.Api.Data
{
    public class SeedRoles
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                var adminRole = new Role { Name = "Admin" };
                await roleManager.CreateAsync(adminRole);
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                var userRole = new Role { Name = "User" };
                await roleManager.CreateAsync(userRole);
            }
        }
    }
}
