using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class SeedDatacs
    {
        public static async Task SeedInitialData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = new List<string>() { "User", "Admin" };

            foreach(var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (roleExists == false)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "John",
                    Email = "John@gmail.com"
                },
                new ApplicationUser()
                {
                    UserName = "Catherine",
                    Email = "Catherine@gmail.com",
                },
                new ApplicationUser()
                {
                    UserName = "Betsy",
                    Email = "Betsy@gmail.com",
                }
            };

            foreach (var user in users)
            {
                if(userManager.FindByEmailAsync(user.Email).Result == null)
                {
                    var result = await userManager.CreateAsync(user, "Password@1234");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }
            }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };

            if (userManager.FindByEmailAsync(adminUser.Email).Result == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Password@1234Password@12345");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
