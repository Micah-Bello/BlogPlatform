using BlogPlatform.Data;
using BlogPlatform.Enums;
using BlogPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext context, 
                            RoleManager<IdentityRole> roleManager, 
                            UserManager<BlogUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task ManageDataAsync()
        {
            await _context.Database.MigrateAsync();

            await SeedRolesAsync();

            await SeedUsersAsync();
        }


        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_context.Users.Any())
            {
                return;
            }

            var adminUser = new BlogUser
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "888-222-1211",
                EmailConfirmed = true
            };

            var modUser = new BlogUser
            {
                Email = "mod@gmail.com",
                UserName = "mod@gmail.com",
                FirstName = "Mod",
                LastName = "User",
                PhoneNumber = "888-222-1515",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(adminUser, "Admin123*");
            await _userManager.CreateAsync(modUser, "Moderator123*");

            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }

        
    }
}
