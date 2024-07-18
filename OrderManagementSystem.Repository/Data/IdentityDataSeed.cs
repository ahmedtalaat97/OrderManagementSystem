using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository.Data
{
    public static class IdentityDataSeed
    {

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole("Admin");
            if (!await roleManager.RoleExistsAsync(adminRole.Name))
            {
                await roleManager.CreateAsync(adminRole);
            }

            var customerRole = new IdentityRole("Customer");
            if (!await roleManager.RoleExistsAsync(customerRole.Name))
            {
                await roleManager.CreateAsync(customerRole);
            }

            var adminUser = new ApplicationUser
            {
                UserName = "Route",
                Email = "Route@example.com",
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                }
            }
        }


    }
}
