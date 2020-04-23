using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Announcement.Api.Controllers;
using Announcement.Api.Data;
using Announcement.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Announcement.Api {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var env = services.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                var context = services.GetRequiredService<AnnouncementContext>();
                var userManager = services.GetRequiredService<UserManager<Employee>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

                //host.Seed(context, userManager, roleManager);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
