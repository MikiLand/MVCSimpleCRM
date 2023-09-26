﻿using Microsoft.AspNetCore.Identity;
using MVCSimpleCRM.Models;
using System.Diagnostics;
using System.Net;

namespace MVCSimpleCRM.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string appUserEmail = "admin@admin.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "Admin",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Name = "Admin",
                        Surname = "Admin",
                    };
                    await userManager.CreateAsync(newAppUser, "!QAZ2wsx");
                    //await userManager.AddToRoleAsync(newAppUser, UserRoles.Admin);
                }
            }
        }
    
    }
}
