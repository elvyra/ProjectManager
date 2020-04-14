using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domains.Entities;

namespace ToDoList.Services
{
    public class DemoIdentityDataSeeder
    {
        public static void SeedIdentityData(ILogger<DemoIdentityDataSeeder> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(logger, roleManager);
            SeedUsers(logger, userManager);
        }

        public static void SeedRoles(ILogger<DemoIdentityDataSeeder> logger, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(UserRoles.ROLE_CLIENT).Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole(UserRoles.ROLE_CLIENT)).Result;
                if (roleResult.Succeeded)
                {
                    logger.LogInformation($"User Role ({UserRoles.ROLE_CLIENT}) seeded to database");
                }
                else
                {
                    logger.LogError($"User Role ({UserRoles.ROLE_CLIENT}) seeding failed");
                }
            } 
            
            if (!roleManager.RoleExistsAsync(UserRoles.ROLE_USER).Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole(UserRoles.ROLE_USER)).Result;
                if (roleResult.Succeeded)
                {
                    logger.LogInformation($"User Role ({UserRoles.ROLE_USER}) seeded to database");
                }
                else
                {
                    logger.LogError($"User Role ({UserRoles.ROLE_USER}) seeding failed");
                }
            }


            if (!roleManager.RoleExistsAsync(UserRoles.ROLE_ADMINISTRATOR).Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole(UserRoles.ROLE_ADMINISTRATOR)).Result;
                if (roleResult.Succeeded)
                {
                    logger.LogInformation($"User Role ({UserRoles.ROLE_ADMINISTRATOR}) seeded to database");
                }
                else
                {
                    logger.LogError($"User Role ({UserRoles.ROLE_ADMINISTRATOR}) seeding failed");
                }
            }
        }

        public static void SeedUsers (ILogger<DemoIdentityDataSeeder> logger, UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("client@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "client@localhost";
                user.Email = "client@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    logger.LogInformation($"User ({user.UserName}) seeded to database");
                    try
                    {
                        userManager.AddToRoleAsync(user, UserRoles.ROLE_CLIENT).Wait();
                        logger.LogInformation($"User ({user.UserName}) added to role ({UserRoles.ROLE_CLIENT}) successfully");
                    }
                    catch (Exception)
                    {
                        logger.LogError($"User ({user.UserName}) adding to role ({UserRoles.ROLE_CLIENT}) failed");
                    }
                }
                else
                {
                    logger.LogError($"User ({user.UserName}) seeding failed");
                }
            } 

             if (userManager.FindByEmailAsync("user@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user@localhost";
                user.Email = "user@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    logger.LogInformation($"User ({user.UserName}) seeded to database");
                    try
                    {
                        userManager.AddToRoleAsync(user, UserRoles.ROLE_USER).Wait();
                        logger.LogInformation($"User ({user.UserName}) added to role ({UserRoles.ROLE_USER}) successfully");
                    }
                    catch (Exception)
                    {
                        logger.LogError($"User ({user.UserName}) adding to role ({UserRoles.ROLE_USER}) failed");
                    }
                }
                else
                {
                    logger.LogError($"User ({user.UserName}) seeding failed");
                }
            } 
            
            if (userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    logger.LogInformation($"User ({user.UserName}) seeded to database");
                    try
                    {
                        userManager.AddToRoleAsync(user, UserRoles.ROLE_ADMINISTRATOR).Wait();
                        logger.LogInformation($"User ({user.UserName}) added to role ({UserRoles.ROLE_ADMINISTRATOR}) successfully");
                    }
                    catch (Exception)
                    {
                        logger.LogError($"User ({user.UserName}) adding to role ({UserRoles.ROLE_ADMINISTRATOR}) failed");
                    }
                }
                else
                {
                    logger.LogError($"User ({user.UserName}) seeding failed");
                }
            }
        }

        
    }
}
