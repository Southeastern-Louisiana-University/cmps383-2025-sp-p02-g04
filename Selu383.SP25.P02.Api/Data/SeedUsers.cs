using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.Roles;
using Selu383.SP25.P02.Api.Features.Users;

namespace Selu383.SP25.P02.Api.Data
{
    public class SeedUsers
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var adminUser = new User { UserName = "galkadi" };
            if (await userManager.FindByNameAsync(adminUser.UserName) == null)
            {
                await userManager.CreateAsync(adminUser, "Password123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var userBob = new User { UserName = "bob" };
            if (await userManager.FindByNameAsync(userBob.UserName) == null)
            {
                await userManager.CreateAsync(userBob, "Password123!");
                await userManager.AddToRoleAsync(userBob, "User");
            }

            var userSue = new User { UserName = "sue" };
            if (await userManager.FindByNameAsync(userSue.UserName) == null)
            {
                await userManager.CreateAsync(userSue, "Password123!");
                await userManager.AddToRoleAsync(userSue, "User");
            }
        }
    }
}
