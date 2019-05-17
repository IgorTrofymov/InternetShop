using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;

namespace InternetShop.WEB.Models
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";

            string pass = "Qwerty9-";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("baseUser") == null)
            {
                roleManager.CreateAsync(new IdentityRole("baseUser"));
            }
            if (await roleManager.FindByNameAsync("superUser") == null)
            {
                roleManager.CreateAsync(new IdentityRole("superUser"));
            }
            if (await roleManager.FindByNameAsync("unregistered") == null)
            {
                roleManager.CreateAsync(new IdentityRole("unregistered"));
            }

            User admin = new User {Email = adminEmail, UserName = adminEmail};
            var result = await userManager.CreateAsync(admin, pass);
            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(admin,
                    new List<string> {"admin", "baseUser", "superUser", "unregistered"});
            }
        }
    }
}