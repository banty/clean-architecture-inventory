using System;
using CleanArchitectureInventory.Catalog.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Persistance
{
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(
             ILogger<ApplicationDbContextInitializer> logger,
             UserManager<ApplicationUser> userManager,
             RoleManager<IdentityRole> roleManager,
             ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        public async Task InitializeAsyn()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while initilizing the database");
                throw;
            }
           

        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task TrySeedAsync()
        {
            var adminRole = new IdentityRole("Admin");

            if(_roleManager.Roles.All(r=>r.Name != adminRole.Name))
            {
               await _roleManager.CreateAsync(adminRole);
            }

            var admin = new ApplicationUser { UserName = "Admin", Email = "Admin@localhost" };
            if(_userManager.Users.All(u=>u.UserName != admin.UserName))
            {
                await _userManager.CreateAsync(admin, "Admin@321");
                if(!string.IsNullOrWhiteSpace(adminRole.Name))
                {
                    await _userManager.AddToRolesAsync(admin, new string[] { adminRole.Name });
                }
            }

            ///:TODO seed default data here if required
        }
    }
}

