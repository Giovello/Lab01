using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Lab01.Data;
using Lab01.Models;

namespace SampleAuth.Data
{
    public static class DbInitializer
    {
        //public static AppSecrets appSecrets { get; set; }
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            //if (roleManager.Roles.Count() > 0)
            //    return 1;

            //// Seed roles
            //int result = await SeedRoles(roleManager);
            //if (result != 0)
            //    return 2;

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  

            // Seed users
            int result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  

            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Manager Role
            var result = await roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
                return 1;  

            // Create Employee Role
            result = await roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
                return 2;  

            return 0;
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Admin User
            var manager = new ApplicationUser
            {
                UserName = "manager@mohawkcollege.ca",
                Email = "manager@mohawkcollege.ca",
                FirstName = "Manager",
                LastName = "Mike",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(manager, System.Environment.GetEnvironmentVariable("AccountPassword"));
            if (!result.Succeeded)
                return 1;  

            // Assign user to Admin role
            result = await userManager.AddToRoleAsync(manager, "Manager");
            if (!result.Succeeded)
                return 2;  

            //// Assign country claim to user
            //result = await userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.Country, "Canada"));
            //if (!result.Succeeded)
            //    return 5;  

            // Create Member User
            var employee = new ApplicationUser
            {
                UserName = "employee@mohawkcollege.ca",
                Email = "employee@mohawkcollege.ca",
                FirstName = "Employee",
                LastName = "Ed",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(employee, System.Environment.GetEnvironmentVariable("AccountPassword"));
            if (!result.Succeeded)
                return 3;  

            // Assign user to Member role
            result = await userManager.AddToRoleAsync(employee, "Employee");
            if (!result.Succeeded)
                return 4;  

            return 0;
        }
    }
}
