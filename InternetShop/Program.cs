using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DAL.Models;
using InternetShop.WEB.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InternetShop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var userManager = services.GetRequiredService<UserManager<User>>();
            //        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //        await RoleInitializer.InitializeAsync(userManager, rolesManager);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
        }

    }
}
