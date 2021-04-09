using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WINTEX.Models.Authentication;

namespace WINTEX.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Researcher.ToString()));
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Users
            var defaultUser = new ApplicationUser
            {
                UserName = "evansp",
                Email = "evansp@byu.edu",
                FirstName = "R Paul",
                LastName = "Evans",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var defaultUser2 = new ApplicationUser
            {
                UserName = "Kerry_Muhlestein",
                Email = "Kerry_Muhlestein@byu.edu",
                FirstName = "Kerry",
                LastName = "Muhlestein",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var defaultUser3 = new ApplicationUser
            {
                UserName = "george_pierce",
                Email = "george_pierce@byu.edu",
                FirstName = "George",
                LastName = "Pierce",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };


            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "M0lecularBiology!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Researcher.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                }

            }

            if (userManager.Users.All(u => u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, "Pearl0fGreatPrice!");
                    await userManager.AddToRoleAsync(defaultUser2, Enums.Roles.Researcher.ToString());
                    await userManager.AddToRoleAsync(defaultUser2, Enums.Roles.Admin.ToString());
                }

            }

            if (userManager.Users.All(u => u.Id != defaultUser3.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser3.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser3, "B33rsheba!");
                    await userManager.AddToRoleAsync(defaultUser3, Enums.Roles.Researcher.ToString());
                    await userManager.AddToRoleAsync(defaultUser3, Enums.Roles.Admin.ToString());
                }

            }


        }
    }
}
