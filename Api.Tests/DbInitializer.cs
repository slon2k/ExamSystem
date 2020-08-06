using Api.Handlers.Auth;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Tests
{
    public static class DbInitializer
    {
        public static void SeedData(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            
            var categories = context.Categories.ToList();
            if (categories.Count == 0)
            {
                try
                {
                    context.Categories.AddRange(GetCategories());
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                
            }

        }

        public static List<Category> GetCategories()
        {
            var categories = new List<Category>();
            for (int i = 1; i <= 6; i++)
            {
                categories.Add(new Category { Title = @$"Title {i}", Description = $@"Description {i}" });
            }
            return categories;
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var adminRole = new IdentityRole();
                adminRole.Name = "admin";
                roleManager.CreateAsync(adminRole).Wait();
            }
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var userRole = new IdentityRole();
                userRole.Name = "user";
                roleManager.CreateAsync(userRole).Wait();
            }
        }

        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@test.com").Result == null)
            {
                var admin = new AppUser() { Email = "admin@test.com", UserName = "admin" };
                IdentityResult result = userManager.CreateAsync(admin, "Pa$$w0rd").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "admin");
                }
            }
            if (userManager.FindByEmailAsync("user@test.com").Result == null)
            {
                var user = new AppUser() { Email = "user@test.com", UserName = "user" };
                IdentityResult result = userManager.CreateAsync(user, "Pa$$w0rd").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "user");
                }
            }            
        }
    }
}
