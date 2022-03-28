using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CLOUD462022.Data
{
    public static class SeedData
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider,
         IConfiguration Configuration)
        {
            //include customized profiles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //define profiles in a strings array
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            //goes trought the array
            //checks if profile already exists
            foreach (var roleName in roleNames)
            {
                // creates profiles and inserts in database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // creates super user with access to app backoffice and admin power
            var poweruser = new IdentityUser
            {
                //gets username and email from appsettings file 
                UserName = Configuration.GetSection("UserSettings")["UserName"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };

            //gets password from appsettings file 
            string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];

            //checks if exists any user with the registed email in appsettings file
            var user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (user == null)
            {
                //creates super user with UserSettings data at appsettings file
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // register the user with the Admin profile 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}

